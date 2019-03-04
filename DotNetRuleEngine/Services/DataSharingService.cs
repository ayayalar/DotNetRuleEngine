using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using DotNetRuleEngine.Interface;
using DotNetRuleEngine.Models;
using DotNetRuleEngineTimeOutException = DotNetRuleEngine.Exceptions.TimeoutException;

namespace DotNetRuleEngine.Services
{
    internal sealed class DataSharingService
    {
        private static readonly Lazy<DataSharingService> DataManager =
            new Lazy<DataSharingService>(() => new DataSharingService(), true);

        private DataSharingService()
        {
        }

        private Lazy<ConcurrentDictionary<string, Task<object>>> AsyncData { get; } =
            new Lazy<ConcurrentDictionary<string, Task<object>>>(
                () => new ConcurrentDictionary<string, Task<object>>(), true);

        private Lazy<ConcurrentDictionary<string, object>> Data { get; } =
            new Lazy<ConcurrentDictionary<string, object>>(
                () => new ConcurrentDictionary<string, object>(), true);

        internal const int DefaultTimeoutInMs = 15000;

        public async Task AddOrUpdateAsync<T>(string key, Task<object> value, IConfiguration<T> configuration)
        {
            var ruleEngineId = GetRuleEngineId(configuration);
            var keyPair = BuildKey(key, ruleEngineId);

            await Task.FromResult(AsyncData.Value.AddOrUpdate(keyPair.First(), v => value, (k, v) => value));
        }

        public async Task<object> GetValueAsync<T>(string key, IConfiguration<T> configuration,
            int timeoutInMs = DefaultTimeoutInMs)
        {
            var timeout = DateTime.Now.AddMilliseconds(timeoutInMs);
            var ruleEngineId = GetRuleEngineId(configuration);
            var keyPair = BuildKey(key, ruleEngineId);

            while (DateTime.Now < timeout)
            {
                AsyncData.Value.TryGetValue(keyPair.First(), out var value);

                if (value != null) return await value;
            }

            throw new DotNetRuleEngineTimeOutException($"Unable to get {key}");
        }

        public void AddOrUpdate<T>(string key, object value, IConfiguration<T> configuration)
        {
            var ruleEngineId = GetRuleEngineId(configuration);
            var keyPair = BuildKey(key, ruleEngineId);

            Data.Value.AddOrUpdate(keyPair.First(), v => value, (k, v) => value);
        }

        public object GetValue<T>(string key, IConfiguration<T> configuration, int timeoutInMs = DefaultTimeoutInMs)
        {
            var timeout = DateTime.Now.AddMilliseconds(timeoutInMs);
            var ruleEngineId = GetRuleEngineId(configuration);
            var keyPair = BuildKey(key, ruleEngineId);

            while (DateTime.Now < timeout)
            {
                Data.Value.TryGetValue(keyPair.First(), out var value);

                if (value != null) return value;
            }

            throw new DotNetRuleEngineTimeOutException($"Unable to get {key}");
        }

        public static DataSharingService GetInstance() => DataManager.Value;

        private static string[] BuildKey(string key, string ruleEngineId) =>
            new[] {string.Join("_", ruleEngineId, key), key};

        private static string GetRuleEngineId<T>(IConfiguration<T> configuration) =>
            ((RuleEngineConfiguration<T>) configuration).RuleEngineId.ToString();
    }
}