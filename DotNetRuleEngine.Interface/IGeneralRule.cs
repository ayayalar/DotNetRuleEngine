using System;
using System.Collections.Generic;

namespace DotNetRuleEngine.Interface
{
    public interface IGeneralRule<T> where T : class, new()
    {
        T Model { get; set; }

        bool IsNested { get; }

        bool IsReactive { get; set; }

        bool IsPreactive { get; set; }

        bool IsExceptionHandler { get; set; }

        Type ObserveRule { get; }

        Exception UnhandledException { get; set; }

        IDependencyResolver Resolve { get; set; }

        IConfiguration<T> Configuration { get; set; }

        IList<object> GetRules();

        void AddRules(params object[] rules);
    }
}