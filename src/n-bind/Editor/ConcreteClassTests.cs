#if N_BIND_TESTS
using N.Package.Bind.fixtures;
using N.Package.Tests;
using NUnit.Framework;

namespace N.Package.Bind.Editor
{
    public class ConcreteClassTests : TestCase
    {
        [Test]
        public void TestResolveFromImpl()
        {
            var registry = new ServiceRegistry();
            registry.Register(new ConcreteClassServiceModule());

            var deps = registry.Resolve<ImplA>();
            Assert(deps != null);
        }
        
        [Test]
        public void TestResolveAuto()
        {
            var registry = new ServiceRegistry();
            registry.Register(new ConcreteClassServiceModule());

            var deps = registry.Resolve<ImplRecursive>();
            Assert(deps != null);
        }

        [Test]
        public void TestNestedResolve()
        {
            var registry = new ServiceRegistry();
            registry.Register(new ConcreteClassServiceModule());

            var deps = registry.Resolve<ConcreteDepAsDep>();
            Assert(deps != null);
            Assert(deps.Deps != null);
            Assert(deps.Deps.C != null);
            Assert(deps.Deps.A != null);
        }
        
        private class ConcreteClassServiceModule : IServiceModule
        {
            public void Register(ServiceRegistry registry)
            {
                registry.Register<ConcreteDepAsDep>();
                registry.Register<ConcreteDeps>();
                registry.Register<ImplA, ImplA>();
                registry.Register<ImplRecursive>();
            }
        }
    }
}
#endif
