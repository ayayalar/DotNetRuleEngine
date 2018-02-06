using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetRuleEngine.Interface;

namespace DotNetRuleEngine.Services
{
    internal sealed class RxRuleService<TK, T> where T : class, new() where TK : IGeneralRule<T>
    {
        private readonly IList<TK> _rules;
        private readonly Lazy<ConcurrentDictionary<Type, IList<TK>>> _proactiveRules;
        private readonly Lazy<ConcurrentDictionary<Type, IList<TK>>> _reactiveRules;
        private readonly Lazy<ConcurrentDictionary<Type, IList<TK>>> _exceptionRules;

        public RxRuleService(IList<TK> rules)
        {
            _rules = rules;
            _proactiveRules = new Lazy<ConcurrentDictionary<Type, IList<TK>>>(CreateProactiveRules, true);
            _reactiveRules = new Lazy<ConcurrentDictionary<Type, IList<TK>>>(CreateReactiveRules, true);
            _exceptionRules = new Lazy<ConcurrentDictionary<Type, IList<TK>>>(CreateExceptionRules, true);
        }

        public ConcurrentDictionary<Type, IList<TK>> GetReactiveRules() => _reactiveRules.Value;

        public ConcurrentDictionary<Type, IList<TK>> GetProactiveRules() => _proactiveRules.Value;

        public ConcurrentDictionary<Type, IList<TK>> GetExceptionRules() => _exceptionRules.Value;


        public IList<TK> FilterRxRules(IList<TK> rules)
        {
            return rules.Where(r => !r.IsReactive && !r.IsProactive && !r.IsExceptionHandler && !r.IsGlobalExceptionHandler).ToList();
        }

        private ConcurrentDictionary<Type, IList<TK>> CreateProactiveRules()
        {
            var rxRules = new ConcurrentDictionary<Type, IList<TK>>();
            GetRxRules(_rules, rxRules, rule => rule.IsProactive);

            return rxRules;
        }

        private ConcurrentDictionary<Type, IList<TK>> CreateReactiveRules()
        {
            var rxRules = new ConcurrentDictionary<Type, IList<TK>>();
            GetRxRules(_rules, rxRules, rule => rule.IsReactive);

            return rxRules;
        }

        private ConcurrentDictionary<Type, IList<TK>> CreateExceptionRules()
        {
            var rxRules = new ConcurrentDictionary<Type, IList<TK>>();
            GetRxRules(_rules, rxRules, rule => rule.IsExceptionHandler);

            return rxRules;
        }

        private static void GetRxRules(IList<TK> rules,
            ConcurrentDictionary<Type, IList<TK>> rxRules, Predicate<TK> predicate)
        {
            Parallel.ForEach(rules, r =>
            {
                if (predicate(r))
                {
                    rxRules.AddOrUpdate(r.ObserveRule, new List<TK> { r }, (type, list) =>
                   {
                       list.Add(r);
                       return list;
                   });
                }
                if (r.IsNested) GetRxRules(r.GetRules().OfType<TK>().ToList(), rxRules, predicate);
            });
        }
    }
}