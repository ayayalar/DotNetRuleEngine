using DotnetRuleEngine.Test.Rules;
using DotNetRuleEngine;
using DotNetRuleEngine.Extensions;
using DotNetRuleEngine.Test.AsyncRules;
using DotNetRuleEngine.Test.Models;
using DotNetRuleEngine.Test.Rules;
using Xunit;

namespace DotnetRuleEngine.Test
{
    public class TestRxRule
    {
        [Fact]
        public void TestReactiveRules()
        {
            var product = new Product();
            var ruleEngineExecutor = RuleEngine<Product>.GetInstance(product);
            ruleEngineExecutor.AddRules(new ProductRule(), new ProductReactiveRule());
            var rr = ruleEngineExecutor.Execute();
            Assert.True(rr.FindRuleResult<ProductReactiveRule>().Data["Ticks"].To<long>() >= rr.FindRuleResult<ProductRule>().Data["Ticks"].To<long>(),
                $"expected {rr.FindRuleResult<ProductReactiveRule>().Data["Ticks"]} actual {rr.FindRuleResult<ProductRule>().Data["Ticks"]}");
        }

        [Fact]
        public void TestPreactiveRules()
        {
            var product = new Product();
            var ruleEngineExecutor = RuleEngine<Product>.GetInstance(product);
            ruleEngineExecutor.AddRules(new ProductRule(), new ProductPreactiveRule());
            var rr = ruleEngineExecutor.Execute();
            Assert.True(rr.FindRuleResult<ProductPreactiveRule>().Data["Ticks"].To<long>() <= rr.FindRuleResult<ProductRule>().Data["Ticks"].To<long>(),
                $"expected {rr.FindRuleResult<ProductPreactiveRule>().Data["Ticks"]} actual {rr.FindRuleResult<ProductRule>().Data["Ticks"]}");
        }

        [Fact]
        public void TestExceptionHandler()
        {
            var product = new Product();
            var ruleEngineExecutor = RuleEngine<Product>.GetInstance(product);
            ruleEngineExecutor.AddRules(new ProductExceptionHandler(), new ProductExceptionThrown());
            var rr = ruleEngineExecutor.Execute();
            Assert.NotNull(rr.FindRuleResult<ProductExceptionHandler>().Error.Exception);
        }        

        [Fact]
        public void TestExceptionHandlerAsync()
        {
            var product = new Product();
            var ruleEngineExecutor = RuleEngine<Product>.GetInstance(product);
            ruleEngineExecutor.AddRules(new ProductExceptionHandlerAsync(), new ProductExceptionThrownAsync());
            var rr = ruleEngineExecutor.ExecuteAsync().Result;
            Assert.NotNull(rr.FindRuleResult<ProductExceptionHandlerAsync>().Error.Exception);
        }

        [Fact]
        public void TestGlobalExceptionHandler()
        {
            var product = new Product();
            var ruleEngineExecutor = RuleEngine<Product>.GetInstance(product);
            ruleEngineExecutor.AddRules(new ProductGlobalExceptionHandler(), new ProductExceptionThrown());
            var rr = ruleEngineExecutor.Execute();
            Assert.NotNull(rr.FindRuleResult<ProductGlobalExceptionHandler>().Error.Exception);
        }

        [Fact]
        public void TestGlobalExceptionHandlerAsync()
        {
            var product = new Product();
            var ruleEngineExecutor = RuleEngine<Product>.GetInstance(product);
            ruleEngineExecutor.AddRules(new ProductGlobalExceptionHandlerAsync(), new ProductExceptionThrownAsync());
            var rr = ruleEngineExecutor.ExecuteAsync().Result;
            Assert.NotNull(rr.FindRuleResult<ProductGlobalExceptionHandlerAsync>().Error.Exception);
        }
    }
}
