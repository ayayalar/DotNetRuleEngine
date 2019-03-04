using System;

namespace DotNetRuleEngine.Exceptions
{
    public class InvalidRuleObjectException : Exception
    {
        public InvalidRuleObjectException()
        {
        }

        public InvalidRuleObjectException(string message) : base(message)
        {
        }

        public InvalidRuleObjectException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}