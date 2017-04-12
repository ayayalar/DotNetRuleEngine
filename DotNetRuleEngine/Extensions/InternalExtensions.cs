using System;
using System.Collections.Generic;
using System.Linq;
using DotNetRuleEngine.Exceptions;
using DotNetRuleEngine.Interface;

namespace DotNetRuleEngine.Extensions
{
    internal static class InternalExtensions
    {
        public static bool CanInvoke<T>(this IGeneralRule<T> rule, T model, bool terminated) where T : class, new() =>
            !rule.Configuration.Skip && rule.Configuration.Constraint.Invoke2(model) && !terminated;

        public static bool Invoke2<T>(this Predicate<T> predicate, T model) =>
            predicate == null || predicate(model);


        public static void AssignRuleName(this IRuleResult ruleResult, string ruleName)
        {
            if (ruleResult != null) ruleResult.Name = ruleResult.Name ?? ruleName;
        }

        public static void Validate<T>(this T model)
        {
            if (model == null) throw new ModelInstanceNotFoundException();
        }

        public static void UpdateRuleEngineConfiguration<T>(this IGeneralRule<T> rule,
            IConfiguration<T> ruleEngineConfiguration) where T : class, new()
        {
            if (ruleEngineConfiguration.Terminate == null && rule.Configuration.Terminate == true)
            {
                ruleEngineConfiguration.Terminate = true;
            }
        }

        public static bool IsRuleEngineTerminated<T>(this IConfiguration<T> ruleEngineConfiguration) where T : class, new()
            => ruleEngineConfiguration.Terminate != null && ruleEngineConfiguration.Terminate.Value;

        public static IList<IGeneralRule<T>> GetRulesWithExecutionOrder<T>(this IEnumerable<IGeneralRule<T>> rules,
            Func<IGeneralRule<T>, bool> condition = null) where T : class, new()
        {
            condition = condition ?? (rule => true);

            return rules.Where(r => r.Configuration.ExecutionOrder.HasValue)
                .Where(condition)
                .OrderBy(r => r.Configuration.ExecutionOrder)
                .ToList();
        }

        public static IList<IGeneralRule<T>> GetRulesWithoutExecutionOrder<T>(this IEnumerable<IGeneralRule<T>> rules,
            Func<IGeneralRule<T>, bool> condition = null) where T : class, new()
        {
            condition = condition ?? (k => true);

            return rules.Where(r => !r.Configuration.ExecutionOrder.HasValue)
                .Where(condition)
                .AsParallel()
                .ToList();
        }
    }
}