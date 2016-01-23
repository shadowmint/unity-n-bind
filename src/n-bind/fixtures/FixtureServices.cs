#if N_BIND_TESTS
using N.Package.Bind;
using UnityEngine;

public class FixtureServices : IServiceModule
{
    private ImplComponent component;

    public FixtureServices(ImplComponent component)
    {
        this.component = component;
    }

    public void Register(ServiceRegistry registry)
    {
        registry.Register<IDepA, ImplA>();
        registry.Register<IDepB, ImplB>();
        registry.Register<IDepC, ImplRecursive>();
        registry.Register<IDepCmp, ImplComponent>(component);
    }
}
#endif
