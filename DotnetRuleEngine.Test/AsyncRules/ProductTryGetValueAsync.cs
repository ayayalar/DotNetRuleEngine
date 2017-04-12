using System.Threading.Tasks;
using DotNetRuleEngine.Extensions;
using DotNetRuleEngine.Interface;
using DotNetRuleEngine.Models;
using DotNetRuleEngine.Test.Models;

namespace DotNetRuleEngine.Test.AsyncRules
{
    class ProductTryGetValueAsync : RuleAsync<Product>
    {
        public override async Task<IRuleResult> InvokeAsync()
        {
            Model.Description = TryGetValueAsync("Description").To<string>();
            return await Task.FromResult(new RuleResult { Name = "ProductRule", Result = Model.Description });
        }
    }
}