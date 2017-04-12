using System;
using System.Threading.Tasks;
using DotNetRuleEngine;
using DotNetRuleEngine.Interface;
using DotNetRuleEngine.Models;
using DotNetRuleEngine.Test.Models;

namespace DotnetRuleEngine.Test.AsyncRules
{
    public class ProductAExecutionOrderRuleAsync : RuleAsync<Product>
    {
        public override Task InitializeAsync()
        {
            Configuration.ExecutionOrder = 2;

            return Task.FromResult<object>(null);
        }

        public override async Task<IRuleResult> InvokeAsync()
        {
            return await RuleResult.CreateAsync(new RuleResult { Result = DateTime.Now.Ticks });
        }
    }
}