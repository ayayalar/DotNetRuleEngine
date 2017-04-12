using System.Diagnostics;
using System.Threading.Tasks;
using DotNetRuleEngine.Interface;
using DotNetRuleEngine.Models;
using DotNetRuleEngine.Test.Models;

namespace DotNetRuleEngine.Test.AsyncRules
{
    public class ProductNestedParallelUpdateA : RuleAsync<Product>
    {
        public override Task InitializeAsync()
        {
            IsParallel = true;

            return Task.FromResult<object>(null);
        }

        public override async Task<IRuleResult> InvokeAsync()
        {
            await Task.Delay(10);
            Debug.WriteLine("ProductNestedParallelUpdateA executed.");
            return await Task.FromResult<IRuleResult>(new RuleResult { Result = "ProductNestedParallelUpdateA executed." });
        }
    }

    public class ProductNestedParallelUpdateB : RuleAsync<Product>
    {
        public override Task InitializeAsync()
        {
            IsParallel = true;
            AddRules(new ProductNestedParallelUpdateB1(), new ProductNestedParallelUpdateB2());

            return Task.FromResult<object>(null);
        }

        public override async Task<IRuleResult> InvokeAsync()
        {
            // await Task.Delay(15);
            Debug.WriteLine("ProductNestedParallelUpdateB executed.");
            return await Task.FromResult<IRuleResult>(new RuleResult { Result = "ProductNestedParallelUpdateB executed." });
        }
    }

    public class ProductNestedParallelUpdateB1 : RuleAsync<Product>
    {
        public override Task InitializeAsync()
        {
            IsParallel = true;
            AddRules(new ProductNestedParallelUpdateB1A());

            return Task.FromResult<object>(null);
        }

        public override async Task<IRuleResult> InvokeAsync()
        {
            await Task.Delay(30);
            Debug.WriteLine("ProductNestedParallelUpdateB1 executed.");
            return await Task.FromResult<IRuleResult>(new RuleResult { Result = "ProductNestedParallelUpdateB1 executed." });
        }
    }

    public class ProductNestedParallelUpdateB1A1 : RuleAsync<Product>
    {
        public override Task InitializeAsync()
        {
            IsParallel = true;
            AddRules(new ProductNestedParallelUpdateB1A1A());

            return Task.FromResult<object>(null);
        }

        public override async Task<IRuleResult> InvokeAsync()
        {
            await Task.Delay(30);
            Debug.WriteLine("ProductNestedParallelUpdateB1A1 executed.");
            return await Task.FromResult<IRuleResult>(new RuleResult { Result = "ProductNestedParallelUpdateB1A1 executed." });
        }
    }

    public class ProductNestedParallelUpdateB1A1A : RuleAsync<Product>
    {
        public override Task InitializeAsync()
        {
            IsParallel = true;
            return Task.FromResult<object>(null);
        }

        public override async Task<IRuleResult> InvokeAsync()
        {
            await Task.Delay(30);
            Debug.WriteLine("ProductNestedParallelUpdateB1A1A executed.");
            return await Task.FromResult<IRuleResult>(new RuleResult { Result = "ProductNestedParallelUpdateB1A1S executed." });
        }
    }

    public class ProductNestedParallelUpdateB1A : RuleAsync<Product>
    {
        public override Task InitializeAsync()
        {
            IsParallel = true;
            AddRules(new ProductNestedParallelUpdateB1A1());

            return Task.FromResult<object>(null);
        }

        public override async Task<IRuleResult> InvokeAsync()
        {
            await Task.Delay(500);
            Debug.WriteLine("ProductNestedParallelUpdateB1A executed.");
            return await Task.FromResult<IRuleResult>(new RuleResult { Result = "ProductNestedParallelUpdateB1A executed." });
        }
    }

    public class ProductNestedParallelUpdateB2 : RuleAsync<Product>
    {
        public override Task InitializeAsync()
        {
            IsParallel = true;

            return Task.FromResult<object>(null);
        }

        public override async Task<IRuleResult> InvokeAsync()
        {
            await Task.Delay(20);
            Debug.WriteLine("ProductNestedParallelUpdateB2 executed.");
            return await Task.FromResult<IRuleResult>(new RuleResult { Result = "ProductNestedParallelUpdateB2 executed." });
        }
    }

    public class ProductNestedParallelUpdateC : RuleAsync<Product>
    {
        public override Task InitializeAsync()
        {
            IsParallel = true;

            return Task.FromResult<object>(null);
        }

        public override async Task<IRuleResult> InvokeAsync()
        {
            await Task.Delay(300);
            Debug.WriteLine("ProductNestedParallelUpdateC executed.");
            return await Task.FromResult<IRuleResult>(new RuleResult { Result = "ProductNestedParallelUpdateC executed." });
        }
    }
}
