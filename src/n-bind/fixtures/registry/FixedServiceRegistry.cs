#if N_BIND_TESTS
using System.Collections.Generic;
using N.Package.Bind.Core.interfaces;

namespace N.Package.Bind.fixtures.registry
{
    public class FixedServiceRegistry : IServiceRegistry
    {
        public Dictionary<System.Type, object> Bound = new Dictionary<System.Type, object>();

        public T Resolve<T>() where T : class
        {
            if (Bound.ContainsKey(typeof(T)))
            {
                return Bound[typeof(T)] as T;
            }

            return null;
        }
    }
}
#endif