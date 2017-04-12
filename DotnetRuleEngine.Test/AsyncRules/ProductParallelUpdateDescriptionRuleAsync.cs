using System.Threading.Tasks;
using DotNetRuleEngine.Interface;
using DotNetRuleEngine.Test.Models;

namespace DotNetRuleEngine.Test.AsyncRules
{
    class ProductParallelUpdateDescriptionRuleAsync : RuleAsync<Product>
    {
        public override Task InitializeAsync()
        {
            IsParallel = true;

            return Task.FromResult<object>(null);
        }

        public override async Task<IRuleResult> InvokeAsync()
        {
            await Task.Delay(10);
            Model.Description = "Description";

            return await Task.FromResult<IRuleResult>(null);
        }
    }
}