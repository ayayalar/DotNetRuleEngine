using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetRuleEngine.Interface;
using DotNetRuleEngine.Models;
using DotNetRuleEngine.Services;

namespace DotNetRuleEngine
{
    /// <summary>
    /// Rule Engine.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class RuleEngine<T> where T : class, new()
    {
        private T _model;
        private IDependencyResolver _dependencyResolver;
        private RuleService<T> _ruleService;
        private AsyncRuleService<T> _asyncRuleService;
        private readonly List<object> _rules = new List<object>();
        private readonly Guid _ruleEngineId = Guid.NewGuid();
        private readonly RuleEngineConfiguration<T> _ruleEngineConfiguration =
            new RuleEngineConfiguration<T>(new Configuration<T>());

        /// <summary>
        /// Rule engine ctor.
        /// </summary>
        private RuleEngine() { }

        /// <summary>
        /// Set dependency resolver
        /// </summary>
        /// <param name="dependencyResolver"></param>
        public void SetDependencyResolver(IDependencyResolver dependencyResolver) => _dependencyResolver = dependencyResolver;

        /// <summary>
        /// Get a new instance of RuleEngine
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="dependencyResolver"></param>
        /// <param name="ruleLogger"></param>
        /// <returns></returns>
        public static RuleEngine<T> GetInstance(T instance = null, IDependencyResolver dependencyResolver = null) =>
            new RuleEngine<T>
            {
                _model = instance,
                _dependencyResolver = dependencyResolver,
            };

        /// <summary>
        /// Used to add rules to nestingRule engine.
        /// </summary>
        /// <param name="rules">Rule(s) list.</param>
        public void AddRules(params object[] rules) => _rules.AddRange(rules);

        /// <summary>
        /// Used to set instance.
        /// </summary>
        /// <param name="instance">_model</param>
        public void SetInstance(T instance) => _model = instance;

        /// <summary>
        /// Used to execute async rules.
        /// </summary>
        /// <returns></returns>
        public async Task<IRuleResult[]> ExecuteAsync()
        {
            if (!_rules.Any()) return await _asyncRuleService.GetAsyncRuleResultsAsync();

            var rules = await new BootstrapService<T>(_model, _ruleEngineId, _dependencyResolver)
                .BootstrapAsync(_rules);

            _asyncRuleService = new AsyncRuleService<T>(_model, rules, _ruleEngineConfiguration);

            await _asyncRuleService.InvokeAsyncRules();

            return await _asyncRuleService.GetAsyncRuleResultsAsync();
        }

        /// <summary>
        /// Used to execute rules.
        /// </summary>
        /// <returns></returns>
        public IRuleResult[] Execute()
        {
            if (!_rules.Any()) return _ruleService.GetRuleResults();

            var rules = new BootstrapService<T>(_model, _ruleEngineId, _dependencyResolver)
                .Bootstrap(_rules);

            _ruleService = new RuleService<T>(_model, rules, _ruleEngineConfiguration);

            _ruleService.Invoke();

            return _ruleService.GetRuleResults();
        }
    }
}