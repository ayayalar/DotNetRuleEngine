using System;

namespace DotNetRuleEngine.Exceptions
{
    public class UnsupportedRuleException : Exception
    {
        public UnsupportedRuleException()
        {
        }

        public UnsupportedRuleException(string message) : base(message)
        {
        }

        public UnsupportedRuleException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}