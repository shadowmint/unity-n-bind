#if N_BIND_TESTS
using N.Package.Bind;

public class FixtureServices : ServiceModule
{
    public override void Register(ServiceRegistry registry)
    {
        registry.Register<IDepA, ImplA>();
        registry.Register<IDepB, ImplB>();
        registry.Register<IDepC, ImplRecursive>();

    }
}
#endif
