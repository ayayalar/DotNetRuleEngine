using DotNetRuleEngine.Interface;
using DotNetRuleEngine.Test.Models;

namespace DotNetRuleEngine.Test.Rules
{
    class ProductNestedErrorRule : Rule<Product>
    {
        public ProductNestedErrorRule()
        {
            AddRules(new ProductChildErrorRule(), new ProductNestedRuleA());
        }
        public override IRuleResult Invoke()
        {
            return null;
        }
    }
}