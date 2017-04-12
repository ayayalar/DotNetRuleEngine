using System;

namespace DotNetRuleEngine.Interface
{
    public interface IConfiguration<T>
    {
        Predicate<T> Constraint { get; set; }

        int? ExecutionOrder { get; set; }

        bool Skip { get; set; }

        bool? Terminate { get; set; }

        bool InvokeNestedRulesFirst { get; set; }

        bool NestedRulesInheritConstraint { get; set; }
    }
}