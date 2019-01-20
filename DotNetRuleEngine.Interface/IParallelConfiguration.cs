using System.Threading;
using System.Threading.Tasks;

namespace DotNetRuleEngine.Interface
{
    public interface IParallelConfiguration<T> where T : class, new()
    {
        TaskCreationOptions TaskCreationOptions { get; set; }

        CancellationTokenSource CancellationTokenSource { get; set; }

        TaskScheduler TaskScheduler { get; set; }

        bool NestedParallelRulesInherit { get; set; }
    }
}
