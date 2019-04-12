using System;
using DotNetRuleEngine.Interface;

namespace DotNetRuleEngine.Models
{
    public class Configuration<T> : IConfiguration<T>
    {
        /// <summary>
        /// Used to set constraint on the rule to be invoked or not.
        /// </summary>
        public Predicate<T> Constraint { get; set; }

        /// <summary>
        /// Used to skip invoking the rule.
        /// </summary>
        public bool Skip { get; set; }

        /// <summary>
        /// Used to terminate the execution of the rule engine.
        /// </summary>
        public bool? Terminate { get; set; }

        /// <summary>
        /// Used to set execution order for the rule.
        /// </summary>
        public int? ExecutionOrder { get; set; }

        /// <summary>
        /// Used to set invoke the nested rules first.
        /// </summary>
        public bool InvokeNestedRulesFirst { get; set; }

        /// <summary>
        /// Used to set whether the nested rules inherits the constraint from the containing rule.
        /// </summary>
        public bool NestedRulesInheritConstraint { get; set; }
    }
}
