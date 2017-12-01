using System;
using System.Threading.Tasks;
using DotNetRuleEngine;
using DotNetRuleEngine.Interface;
using DotNetRuleEngine.Models;
using DotNetRuleEngine.Test.Models;

namespace DotnetRuleEngine.Test.Rules
{
    internal class ProductGlobalExceptionHandler : Rule<Product>
    {
        public override void Initialize()
        {
            IsGlobalExceptionHandler = true;
        }

        public override IRuleResult Invoke()
        {
            var ruleResult = new RuleResult();

            if (UnhandledException?.GetType() == typeof(Exception))
            {
                ruleResult.Error = new Error { Exception = UnhandledException };
            }

            return ruleResult;
        }
    }

    internal class ProductGlobalExceptionHandlerAsync : RuleAsync<Product>
    {
        public override Task InitializeAsync()
        {
            IsGlobalExceptionHandler = true;
            return base.InitializeAsync();
        }

        public override Task<IRuleResult> InvokeAsync()
        {
            var ruleResult = new RuleResult();

            if (UnhandledException?.GetType() == typeof(Exception))
            {
                ruleResult.Error = new Error { Exception = UnhandledException };
            }

            return RuleResult.CreateAsync(ruleResult);
        }
    }
}
