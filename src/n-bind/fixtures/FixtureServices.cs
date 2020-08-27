#if N_BIND_TESTS
namespace N.Package.Bind.fixtures
{
    public class FixtureServices : IServiceModule
    {
        private readonly ImplComponent _component;

        public FixtureServices(ImplComponent component)
        {
            this._component = component;
        }

        public void Register(ServiceRegistry registry)
        {
            registry.Register<IDepA, ImplA>();
            registry.Register<IDepB, ImplB>();
            registry.Register<IDepC, ImplRecursive>();
            registry.Register<IDepCmp, ImplComponent>(_component);
            registry.Register<ImplAutoDep>();
        }
    }
}
#endif