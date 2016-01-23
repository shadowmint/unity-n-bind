#if N_BIND_TESTS
using NUnit.Framework;
using N.Package.Bind.Core;

public class PropertyBinderTests : N.Tests.Test
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
        registry.bound[typeof(IDepA)] = impl;
        var instance = new PropertyBinder(registry);
        var sample = new SampleClass();
        instance.Bind(sample);
        Assert(sample.instance == impl);
    }
}
#endif
