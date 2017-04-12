using System;
using System.Collections.Generic;

namespace DotNetRuleEngine.Interface
{
    public interface IDependencyResolver
    {
        object GetService(Type serviceType);

        IEnumerable<object> GetServices(Type serviceType);
    }
}
