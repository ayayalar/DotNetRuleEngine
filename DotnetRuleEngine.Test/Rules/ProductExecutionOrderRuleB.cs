using DotNetRuleEngine.Interface;
using DotNetRuleEngine.Models;
using DotNetRuleEngine.Test.Models;

namespace DotNetRuleEngine.Test.Rules
{
    internal class ProductExecutionOrderRuleB : Rule<Product>
    {
        public override void Initialize()
        {
            Configuration.ExecutionOrder = 1;
        }

        public override IRuleResult Invoke()
        {
            return new RuleResult();
        }
    }
}