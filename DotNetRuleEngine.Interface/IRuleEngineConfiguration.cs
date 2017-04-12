using System;

namespace DotNetRuleEngine.Interface
{
    public interface IRuleEngineConfiguration<T> : IConfiguration<T>
    {
        Guid RuleEngineId { get; set; }
    }
}