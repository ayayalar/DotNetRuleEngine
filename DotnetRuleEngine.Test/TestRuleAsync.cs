using System.Linq;
using DotnetRuleEngine.Test.AsyncRules;
using DotNetRuleEngine;
using DotNetRuleEngine.Extensions;
using DotNetRuleEngine.Test.AsyncRules;
using DotNetRuleEngine.Test.Models;
using Xunit;

namespace DotnetRuleEngine.Test
{
    public class TestRuleAsync
    {
        [Fact]
        public void TestInvokeAsync()
        {
            var ruleEngineExecutor = RuleEngine<Product>.GetInstance(new Product());
            ruleEngineExecutor.AddRules(new ProductRuleAsync());
            var ruleResults = ruleEngineExecutor.ExecuteAsync().Result;
            Assert.Equal("Product Description", ruleResults.First().Result);
        }

        [Fact]
        public void TestBeforeInvokeAsync()
        {
            var ruleEngineExecutor = RuleEngine<Product>.GetInstance(new Product());
            ruleEngineExecutor.AddRules(new ProductRuleAsync());
            var ruleResults = ruleEngineExecutor.ExecuteAsync().Result;

            object value;
            ruleResults.First().Data.TryGetValue("Description", out value);
            Assert.Equal("Description", value);
        }

        [Fact]
        public void TestAfterInvokeAsync()
        {
            var ruleEngineExecutor = RuleEngine<Product>.GetInstance(new Product());
            ruleEngineExecutor.AddRules(new ProductTerminateAsyncA(), new ProductTerminateAsyncB());
            var ruleResults = ruleEngineExecutor.ExecuteAsync().Result;
            Assert.Equal(1, ruleResults.Length);
        }

        [Fact]
        public void TestSkipAsync()
        {
            var ruleEngineExecutor = RuleEngine<Product>.GetInstance(new Product());
            ruleEngineExecutor.AddRules(new ProductSkipAsync());
            var ruleResults = ruleEngineExecutor.ExecuteAsync().Result;
            Assert.False(ruleResults.Any());
        }

        [Fact]
        public void TestTerminateAsync()
        {
            var ruleEngineExecutor = RuleEngine<Product>.GetInstance(new Product());
            ruleEngineExecutor.AddRules(new ProductTerminateAsyncA(), new ProductTerminateAsyncB());
            var ruleResults = ruleEngineExecutor.ExecuteAsync().Result;
            Assert.Equal(1, ruleResults.Length);
        }

        [Fact]
        public void TestConstraintAsync()
        {
            var ruleEngineExecutor = RuleEngine<Product>.GetInstance(new Product());
            ruleEngineExecutor.AddRules(new ProductConstraintAsyncA(), new ProductConstraintAsyncB());
            var ruleResults = ruleEngineExecutor.ExecuteAsync().Result;
            Assert.Equal(1, ruleResults.Length);
        }

        [Fact]
        public void TestTryAddTryGetValueAsync()
        {
            var ruleEngineExecutor = RuleEngine<Product>.GetInstance(new Product());
            ruleEngineExecutor.AddRules(new ProductTryAddAsync(), new ProductTryGetValueAsync());
            var ruleResults = ruleEngineExecutor.ExecuteAsync().Result;
            Assert.Equal("Product Description", ruleResults.First().Result);
        }

        [Fact]
        public void TestExecutionOrder()
        {
            var ruleResults = RuleEngine<Product>.GetInstance(new Product())
                .ApplyRules(new ProductAExecutionOrderRuleAsync(), new ProductBExecutionOrderRuleAsync())
                .ExecuteAsync().Result;

            var productBExecutionOrderRuleAsync = ruleResults.FindRuleResult<ProductBExecutionOrderRuleAsync>();
            var productAExecutionOrderRuleAsync = ruleResults.FindRuleResult<ProductAExecutionOrderRuleAsync>();

            Assert.True(productBExecutionOrderRuleAsync.Result.To<long>() <= productAExecutionOrderRuleAsync.Result.To<long>());
        }
    }
}
