#if N_BIND_TESTS
using N.Package.Bind.fixtures;
using N.Package.Bind.fixtures.registry;
using N.Package.Tests;
using NUnit.Framework;

namespace N.Package.Bind.Core.Editor
{
    public class PropertyBinderTests : TestCase
    {
        [Test]
        public void test_bind_no_properties()
        {
            var registry = new EmptyServiceRegistry();
            var instance = new PropertyBinder(registry);
            var sample = new SampleClass();
            instance.Bind(sample);
            Assert(sample.instance == null);
        }

        [Test]
        public void test_bind_property()
        {
            var registry = new FixedServiceRegistry();
            var impl = new ImplA();
            registry.Bound[typeof(IDepA)] = impl;
            var instance = new PropertyBinder(registry);
            var sample = new SampleClass();
            instance.Bind(sample);
            Assert(sample.instance == impl);
        }
    }
}
#endif