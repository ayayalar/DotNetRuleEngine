using DotNetRuleEngine.Interface;
using DotNetRuleEngine.Test.Models;

namespace DotNetRuleEngine.Test.Rules
{
    class ProductNestedRuleB : Rule<Product>
    {
        public override void Initialize()
        {
            Configuration.ExecutionOrder = 1;
        }
        public ProductNestedRuleB()
        {
            AddRules(new ProductNestedRuleC());
        }
        public override IRuleResult Invoke()
        {
            return null;
        }
    }
}