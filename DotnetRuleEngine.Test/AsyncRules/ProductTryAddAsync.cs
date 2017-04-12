using System.Threading.Tasks;
using DotNetRuleEngine.Interface;
using DotNetRuleEngine.Test.Models;

namespace DotNetRuleEngine.Test.AsyncRules
{
    class ProductTryAddAsync : RuleAsync<Product>
    {        
        public override async Task<IRuleResult> InvokeAsync()
        {
            await TryAddAsync("Description", Task.FromResult<object>("Product Description"));
            return await Task.FromResult<IRuleResult>(null);
        }
    }
}