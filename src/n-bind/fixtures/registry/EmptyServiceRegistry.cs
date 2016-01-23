#if N_BIND_TESTS
using N.Package.Bind.Core;

public class EmptyServiceRegistry : IServiceRegistry
{
    public T Resolve<T>() where T : class
    {
        return null;
    }
}
#endif
