using System;
using System.Collections.Generic;
using System.Linq;
using DotNetRuleEngine.Extensions;
using DotNetRuleEngine.Interface;

namespace DotNetRuleEngine.Services
{
    internal class RuleService<T> where T : class, new()
    {
        private readonly IList<IRule<T>> _rules;
        private readonly IRuleEngineConfiguration<T> _ruleEngineConfiguration;
        private readonly RxRuleService<IRule<T>, T> _rxRuleService;
        private readonly ICollection<IRuleResult> _ruleResults = new List<IRuleResult>();

        public RuleService(IList<IRule<T>> rules,
            IRuleEngineConfiguration<T> ruleEngineConfiguration)
        {
            _rules = rules;
            _rxRuleService = new RxRuleService<IRule<T>, T>(_rules);
            _ruleEngineConfiguration = ruleEngineConfiguration;
        }

        public void Invoke() => Execute(_rxRuleService.FilterRxRules(_rules));

        public IRuleResult[] GetRuleResults() => _ruleResults.ToArray();

        private void Execute(IList<IRule<T>> rules)
        {
            foreach (var rule in OrderByExecutionOrder(rules))
            {
                InvokeNestedRules(rule.Configuration.InvokeNestedRulesFirst, rule);

                if (rule.CanInvoke() && !_ruleEngineConfiguration.IsRuleEngineTerminated())
                {
                    InvokeProactiveRules(rule);

                    try
                    {
                        rule.BeforeInvoke();
                        var ruleResult = rule.Invoke();
                        rule.AfterInvoke();

                        AddToRuleResults(ruleResult, rule.GetType().Name);
                    }

                    catch (Exception exception)
                    {
                        rule.UnhandledException = exception;
                        if (_rxRuleService.GetExceptionRules().ContainsKey(rule.GetType()))
                        {
                            InvokeExceptionRules(rule);
                        }
                        else
                        {
                            var globalExceptionHandler = _rules.GetGlobalExceptionHandler();

                            if (globalExceptionHandler is IRule<T>)
                            {
                                globalExceptionHandler.UnhandledException = exception;
                                Execute(new List<IRule<T>> { (IRule<T>)globalExceptionHandler });
                            }
                            else
                            {
                                throw;
                            }                            
                        }
                    }

                    rule.UpdateRuleEngineConfiguration(_ruleEngineConfiguration);

                    InvokeReactiveRules(rule);
                }

                InvokeNestedRules(!rule.Configuration.InvokeNestedRulesFirst, rule);
            }
        }

        private void InvokeReactiveRules(IGeneralRule<T> rule)
        {
            if (_rxRuleService.GetReactiveRules().ContainsKey(rule.GetType()))
            {
                Execute(_rxRuleService.GetReactiveRules()[rule.GetType()].ToList());
            }
        }

        private void InvokeProactiveRules(IRule<T> rule)
        {
            if (_rxRuleService.GetProactiveRules().ContainsKey(rule.GetType()))
            {
                Execute(_rxRuleService.GetProactiveRules()[rule.GetType()].ToList());
            }
        }

        private void InvokeExceptionRules(IRule<T> rule)
        {
            var exceptionRules = _rxRuleService.GetExceptionRules()[rule.GetType()]
                .Select(r =>
                {
                    r.UnhandledException = rule.UnhandledException;
                    return r;
                });

            Execute(exceptionRules.ToList());
        }

        private void AddToRuleResults(IRuleResult ruleResult, string ruleName)
        {
            ruleResult.AssignRuleName(ruleName);
            if (ruleResult != null) _ruleResults.Add(ruleResult);
        }

        private void InvokeNestedRules(bool invokeNestedRules, IRule<T> rule)
        {
            if (invokeNestedRules && rule.IsNested)
            {
                Execute(_rxRuleService.FilterRxRules(OrderByExecutionOrder(rule.GetRules().OfType<IRule<T>>().ToList())));
            }
        }

        private static IList<IRule<T>> OrderByExecutionOrder(IList<IRule<T>> rules)
        {
            return rules.GetRulesWithExecutionOrder().OfType<IRule<T>>()
                .Concat(rules.GetRulesWithoutExecutionOrder().OfType<IRule<T>>())
                .ToList();
        }
    }
}
