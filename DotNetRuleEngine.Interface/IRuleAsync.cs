using System.Threading.Tasks;

namespace DotNetRuleEngine.Interface
{
    public interface IRuleAsync<T> : IGeneralRule<T> where T : class, new()
    {
        bool IsParallel { get; set; }

        IParallelConfiguration<T> ParallelConfiguration { get; set; }

        void ObserveRule<TK>() where TK : IRuleAsync<T>;

        Task InitializeAsync();

        Task BeforeInvokeAsync();

        Task AfterInvokeAsync();
        
        Task<IRuleResult> InvokeAsync();

        Task<object> TryGetValueAsync(string key, int timeoutInMs);

        Task TryAddAsync(string key, Task<object> value);
    }
}