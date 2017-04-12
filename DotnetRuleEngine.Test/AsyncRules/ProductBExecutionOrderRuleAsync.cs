using System;
using System.Threading.Tasks;
using DotNetRuleEngine.Interface;
using DotNetRuleEngine.Models;
using DotNetRuleEngine.Test.Models;

namespace DotNetRuleEngine.Test.AsyncRules
{
    public class ProductBExecutionOrderRuleAsync : RuleAsync<Product>
    {
        public override Task InitializeAsync()
        {
            Configuration.ExecutionOrder = 1;

            return Task.FromResult<object>(null);
        }

        public override async Task<IRuleResult> InvokeAsync()
        {
            return await RuleResult.CreateAsync(new RuleResult { Result = DateTime.Now.Ticks });
        }
    }
}