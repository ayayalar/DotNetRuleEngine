using System;
using DotNetRuleEngine.Interface;

namespace DotNetRuleEngine.Models
{
    public class Configuration<T> : IConfiguration<T>
    {
        public Predicate<T> Constraint { get; set; }

        public bool Skip { get; set; }

        public bool? Terminate { get; set; }

        public int? ExecutionOrder { get; set; }

        public bool InvokeNestedRulesFirst { get; set; }

        public bool NestedRulesInheritConstraint { get; set; }
    }
}
