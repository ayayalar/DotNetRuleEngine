using DotNetRuleEngine.Interface;
using DotNetRuleEngine.Test.Models;

namespace DotNetRuleEngine.Test.Rules
{
    class ProductNestedRule : Rule<Product>
    {
        public override void Initialize()
        {
            AddRules(new ProductNestedRuleA(), new ProductNestedRuleB());
        }

        public override IRuleResult Invoke()
        {
            return null;
        }
    }
}