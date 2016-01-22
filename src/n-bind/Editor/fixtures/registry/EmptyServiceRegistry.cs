#if N_BIND_TESTS
using N.Package.Bind.Core;

public class EmptyServiceRegistry : IServiceRegistry
{
    public T Resolve<T>() where T : class
    {
        N._.Log("Request for {0}", typeof(T));
        return null;
    }
}
#endif
