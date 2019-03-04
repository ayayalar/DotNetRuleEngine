using DotNetRuleEngine;
using DotNetRuleEngine.Test.AsyncRules;
using DotNetRuleEngine.Test.Models;
using System.Linq;
using Xunit;

namespace DotnetRuleEngine.Test
{
    public class TestParallelRule
    {
        [Fact]
        public void TestParallelRules()
        {
            var product = new Product();
            var engineExecutor = RuleEngine<Product>.GetInstance(product);
            var ruleEngineExecutor = engineExecutor;

            ruleEngineExecutor.AddRules(
                new ProductParallelUpdateNameRuleAsync(),
                new ProductParallelUpdateDescriptionRuleAsync(),
                new ProductParallelUpdatePriceRuleAsync());

            var ruleResults = ruleEngineExecutor.ExecuteAsync().Result;

            Assert.NotNull(ruleResults);
            Assert.Equal("Product", product.Name);
            Assert.Equal(0.0m, product.Price);
            Assert.Equal("Description", product.Description);
        }

        [Fact]
        public void TestNestedParallelRules()
        {
            var product = new Product();
            var engineExecutor = RuleEngine<Product>.GetInstance(product);
            var ruleEngineExecutor = engineExecutor;

            ruleEngineExecutor.AddRules(
                new ProductNestedParallelUpdateA(),
                new ProductNestedParallelUpdateB(),
                new ProductNestedParallelUpdateC());

            var ruleResults = ruleEngineExecutor.ExecuteAsync().Result;
            Assert.Equal(8, ruleResults.Count());
        }
    }
}
