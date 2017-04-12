using DotNetRuleEngine;
using DotNetRuleEngine.Extensions;
using DotNetRuleEngine.Test.AsyncRules;
using DotNetRuleEngine.Test.Models;
using Xunit;

namespace DotnetRuleEngine.Test
{
    public class TestNestedRuleAsync
    {
        [Fact]
        public void TestAsyncNestedRules()
        {
            var ruleEngineExecutor = RuleEngine<Product>.GetInstance(new Product());

            ruleEngineExecutor.AddRules(new ProductNestedRuleAsync());

            var ruleResults = ruleEngineExecutor.ExecuteAsync().Result;

            Assert.NotNull(ruleResults);
            Assert.Equal("ProductNestedRuleAsyncC", ruleResults.FindRuleResult<ProductNestedRuleAsyncC>().Name);
        }
    }
}
