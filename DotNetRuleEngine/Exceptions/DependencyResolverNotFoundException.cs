using System;

namespace DotNetRuleEngine.Exceptions
{
    public class DependencyResolverNotFoundException : Exception
    {
        public DependencyResolverNotFoundException()
        {
        }

        public DependencyResolverNotFoundException(string message) : base(message)
        {
        }

        public DependencyResolverNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}