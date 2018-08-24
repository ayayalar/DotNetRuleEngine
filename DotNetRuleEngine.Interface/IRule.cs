namespace DotNetRuleEngine.Interface
{
    public interface IRule<T> : IGeneralRule<T> where T : class, new()
    {
        void ObserveRule<TK>() where TK : IRule<T>;

        void Initialize();

        void BeforeInvoke();
        
        void AfterInvoke();
        
        IRuleResult Invoke();

        object TryGetValue(string key, int timeoutInMs);

        void TryAdd(string key, object value);        
    }
}
