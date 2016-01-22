#if N_BIND_TESTS
using N.Package.Bind.Core;
using System.Collections.Generic;

public class FixedServiceRegistry : IServiceRegistry
{
    public Dictionary<System.Type, object> bound = new Dictionary<System.Type, object>();
    public T Resolve<T>() where T : class
    {
        if (bound.ContainsKey(typeof(T)))
        {
            N._.Log("resolved... {0} to {1}", bound[typeof(T)], typeof(T));
            N._.Log(" ----> {0}", bound[typeof(T)] as T);
            return bound[typeof(T)] as T;
        }
        return null;
    }
}
#endif
