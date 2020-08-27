#if N_BIND_TESTS
using N.Package.Bind.fixtures;
using N.Package.Tests;
using NUnit.Framework;

namespace N.Package.Bind.Editor
{
    public class DefaultServiceRegistryTests : TestCase
    {
        /// Reset registry and return a new one to test with
        private ServiceRegistry fixture()
        {
            var instance = Registry.Default;
            var cmp = this.SpawnComponent<ImplComponent>();
            instance.Reset();
            instance.Register(new FixtureServices(cmp));
            return instance;
        }

        [Test]
        public void test_auto_registration_of_implemented_interfaces()
        {
            var instance = fixture();
            Assert(instance.Resolve<IAutoDepA>() != null);
            Assert(instance.Resolve<IAutoDepB>() != null);
            Assert(instance.Resolve<IAutoDepC<int>>() != null);
            Assert(instance.Resolve<IAutoDepC<float>>() != null);
            Assert(instance.Resolve<IAutoDepC<bool>>() == null);
            this.TearDown();
        }
        
        [Test]
        public void test_resolve_class()
        {
            var instance = fixture().CreateInstance<SampleClass>();
            Assert(instance != null);
            Assert(instance.instance != null);
            this.TearDown();
        }

        [Test]
        public void test_resolve_interface()
        {
            var instance = fixture().Resolve<IDepB>();
            Assert(instance != null);
            Assert(instance.A != null);
            this.TearDown();
        }

        [Test]
        public void test_resolve_gameobject()
        {
            var instance = fixture().Resolve<IDepCmp>();
            Assert(instance != null);
            Assert(instance.B != null);
            this.TearDown();
        }

        [Test]
        public void test_resolve_recursive()
        {
            var instance = fixture().Resolve<IDepC>();
            Assert(instance != null);
            Assert(instance.A != null);
            Assert(instance.B != null);
            Assert(instance.Component != null);
            Assert(instance.B.A == instance.A);
            Assert(instance.C == instance);
            this.TearDown();
        }
    }
}
#endif