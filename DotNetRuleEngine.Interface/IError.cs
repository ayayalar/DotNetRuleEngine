using System;

namespace DotNetRuleEngine.Interface
{
    public interface IError
    {
        string Message { get; set; }

        Exception Exception { get; set; }
    }
}