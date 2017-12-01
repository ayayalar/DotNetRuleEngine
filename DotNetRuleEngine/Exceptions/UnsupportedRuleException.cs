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

    public class GlobalHandlerException : Exception
    {
        public GlobalHandlerException()
        {
        }

        public GlobalHandlerException(string message) : base(message)
        {
        }

        public GlobalHandlerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}