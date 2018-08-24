using System;
using System.Threading.Tasks;
using DotNetRuleEngine.Interface;
using DotNetRuleEngine.Models;
using DotNetRuleEngine.Test.Models;

namespace DotNetRuleEngine.Test.AsyncRules
{
    internal class ProductExceptionHandlerAsync : RuleAsync<Product>
    {
        public override Task InitializeAsync()
        {
            IsExceptionHandler = true;
            ObserveRule<ProductExceptionThrownAsync>();

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
