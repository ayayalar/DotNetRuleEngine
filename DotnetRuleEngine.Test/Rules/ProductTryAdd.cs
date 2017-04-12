using DotNetRuleEngine.Interface;
using Product = DotNetRuleEngine.Test.Models.Product;

namespace DotNetRuleEngine.Test.Rules
{
    class ProductTryAdd : Rule<Product>
    {
        public override void Initialize()
        {
            TryAdd("Description1", "Product Description1");
        }

        public override void BeforeInvoke()
        {
            TryAdd("Description2", "Product Description2");
        }

        public override IRuleResult Invoke()
        {
            TryAdd("Description3", "Product Description3");
            return null;
        }

        public override void AfterInvoke()
        {
            TryAdd("Description4", "Product Description4");
        }
    }
}