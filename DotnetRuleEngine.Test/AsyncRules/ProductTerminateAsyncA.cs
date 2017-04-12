using System.Threading.Tasks;
using DotNetRuleEngine.Interface;
using DotNetRuleEngine.Models;
using DotNetRuleEngine.Test.Models;

namespace DotNetRuleEngine.Test.AsyncRules
{
    class ProductTerminateAsyncA : RuleAsync<Product>
    {
        public override Task AfterInvokeAsync()
        {
            Configuration.Terminate = true;
            return Task.FromResult<object>(null);
        }

        public override async Task<IRuleResult> InvokeAsync()
        {
            Model.Description = "Product Description";
            return await Task.FromResult(new RuleResult { Name = "ProductRule", Result = Model.Description });
        }
    }
}