using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetRuleEngine.Interface;
using DotNetRuleEngine.Models;
using DotNetRuleEngine.Services;

namespace DotNetRuleEngine
{
    public abstract class RuleAsync<T> : IRuleAsync<T> where T : class, new()
    {
        private IList<object> Rules { get; set; } = new List<object>();

        public T Model { get; set; }

        public bool IsParallel { get; set; }

        public bool IsNested => Rules.Any();

        public bool IsReactive { get; set; }

        public bool IsPreactive { get; set; }

        public Type ObserveRule { get; set; }

        public bool IsExceptionHandler { get; set; }

        public Exception UnhandledException { get; set; }

        public IDependencyResolver Resolve { get; set; }

        public IConfiguration<T> Configuration { get; set; } = new Configuration<T>();

        public async Task<object> TryGetValueAsync(string key, int timeoutInMs = DataSharingService.DefaultTimeoutInMs) => 
            await DataSharingService.GetInstance().GetValueAsync(key, Configuration, timeoutInMs);

        public async Task TryAddAsync(string key, Task<object> value) => 
            await DataSharingService.GetInstance().AddOrUpdateAsync(key, value, Configuration);

        public IList<object> GetRules() => Rules;

        public void AddRules(params object[] rules) => Rules = rules.ToList();        

        public virtual async Task InitializeAsync() => await Task.FromResult<object>(null);

        public virtual async Task BeforeInvokeAsync() => await Task.FromResult<object>(null);

        public virtual async Task AfterInvokeAsync() => await Task.FromResult<object>(null);

        public abstract Task<IRuleResult> InvokeAsync();
    }
}
