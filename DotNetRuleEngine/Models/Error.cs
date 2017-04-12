using DotNetRuleEngine.Interface;
using System;

namespace DotNetRuleEngine.Models
{
    public class Error : IError
    {
        public string Message { get; set; }

        public Exception Exception { get; set; }
    }
}
