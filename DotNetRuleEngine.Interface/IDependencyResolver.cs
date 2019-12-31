using System;

namespace DotNetRuleEngine.Interface
{
    public interface IDependencyResolver
    {
        object GetService(Type serviceType);
    }
}
