#if N_BIND_TESTS
using N.Package.Bind.fixtures;
using N.Package.Tests;
using NUnit.Framework;

namespace N.Package.Bind.Editor
{
    public class TestServiceModule : IServiceModule
    {
        public void Register(ServiceRegistry registry)
        {
            registry.Register<IDepA, ImplA>();
        }
    }

    public class ServiceRegistryTests : TestCase
    {
        [Test]
        public void test_manual_registration()
        {
            var registry = new ServiceRegistry();
            registry.Register<IDepA, ImplA>();
        }

        [Test]
        public void test_manual_rebind()
        {
            var registry = new ServiceRegistry();
            registry.Register<IDepA, ImplA>();
            registry.Clear<IDepA>();
            registry.Register<IDepA, ImplA2>();
        }

        [Test]
        public void test_duplicate_registration()
        {
            var registry = new ServiceRegistry();
            registry.Register<IDepA, ImplA>();
            try
            {
                registry.Register<IDepA, ImplA2>();
                Unreachable();
            }
            catch (BindException err)
            {
                Assert(err.ErrorCode == BindError.DuplicateBinding);
            }
        }

        [Test]
        public void test_manual_register_gameobject()
        {
            var registry = new ServiceRegistry();
            var rp = this.SpawnComponent<ImplComponent>();
            registry.Register<IDepCmp, ImplComponent>(rp);
            this.TearDown();
        }

        [Test]
        public void test_service_module_registration()
        {
            var registry = new ServiceRegistry();
            registry.Register(new TestServiceModule());
        }
    }
}
#endif