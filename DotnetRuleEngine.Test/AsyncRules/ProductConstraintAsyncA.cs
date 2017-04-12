using System.Threading.Tasks;
using DotNetRuleEngine.Interface;
using DotNetRuleEngine.Models;
using DotNetRuleEngine.Test.Models;

namespace DotNetRuleEngine.Test.AsyncRules
{
    class ProductConstraintAsyncA : RuleAsync<Product>
    {
        public override Task InitializeAsync()
        {
            Configuration.Constraint = product => product.Description == "Description";

            return Task.FromResult<object>(null);
        }

        public override async Task<IRuleResult> InvokeAsync()
        {
            Model.Description = "Product Description";
            return await RuleResult.CreateAsync(new RuleResult { Name = "ProductRule", Result = Model.Description });
        }        
    }
}