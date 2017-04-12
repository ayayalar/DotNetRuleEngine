using System;
using DotNetRuleEngine.Interface;
using DotNetRuleEngine.Models;
using DotNetRuleEngine.Test.Models;

namespace DotNetRuleEngine.Test.Rules
{
    internal class ProductRule : Rule<Product>
    {
        public override void BeforeInvoke()
        {
            TryAdd("Key", "Value");
        }

        public override IRuleResult Invoke()
        {
            Model.Description = "Product Description";
            TryAdd("Ticks", DateTime.Now.Ticks);
            return new RuleResult { Name = "ProductRule", Result = Model.Description, Data = { { "Key", TryGetValue("Key") }, { "Ticks", TryGetValue("Ticks") } } };
        }
    }
}