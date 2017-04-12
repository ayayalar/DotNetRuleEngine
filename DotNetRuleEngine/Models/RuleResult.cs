using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetRuleEngine.Interface;

namespace DotNetRuleEngine.Models
{
    public class RuleResult : IRuleResult
    {
        public RuleResult()
        {
            Data = new Dictionary<string, object>();
        }

        public string Name { get; set; }

        public object Result { get; set; }

        public Dictionary<string, object> Data { get; set; }

        public IError Error { get; set; }        

        public static Task<IRuleResult> Nil()
        {
            return Task.FromResult<IRuleResult>(null);
        }

        public static async Task<IRuleResult> CreateAsync(RuleResult ruleResult)
        {
            return await Task.FromResult(ruleResult);
        }
    }
}
