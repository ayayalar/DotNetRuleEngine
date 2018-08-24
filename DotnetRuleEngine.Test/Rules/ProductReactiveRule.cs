using System;
using DotNetRuleEngine.Interface;
using DotNetRuleEngine.Models;
using DotNetRuleEngine.Test.Models;

namespace DotNetRuleEngine.Test.Rules
{
    internal class ProductReactiveRule : Rule<Product>
    {
        public override void Initialize()
        {
            IsReactive = true;
            ObserveRule<ProductRule>();
        }

        public override IRuleResult Invoke()
        {
            TryAdd("Ticks", DateTime.Now.Ticks);
            return new RuleResult { Result = Model.Description, Data = { { "Ticks", TryGetValue("Ticks") } } };
        }
    }
}