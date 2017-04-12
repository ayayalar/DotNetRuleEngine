using DotNetRuleEngine.Interface;
using DotNetRuleEngine.Models;
using DotNetRuleEngine.Test.Models;

namespace DotNetRuleEngine.Test.Rules
{
    class ProductNestedRuleA : Rule<Product>
    {
        public override void Initialize()
        {
            Configuration.ExecutionOrder = 2;
        }

        public override IRuleResult Invoke()
        {
            Model.Description = "Product Description";

            return new RuleResult { Name = "ProductNestedRuleA", Result = Model.Description };
        }
    }
}