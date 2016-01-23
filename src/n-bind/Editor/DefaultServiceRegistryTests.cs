#if N_BIND_TESTS
using NUnit.Framework;
using N.Package.Bind;
using N;

public class DefaultServiceRegistryTests : N.Tests.Test
{
    /// Reset registry and return a new one to test with
    private ServiceRegistry fixture() {
        var instance = Registry.Default;
        instance.Reset();
        instance.Register(new FixtureServices());
        return instance;
    }

    [Test]
    public void test_resolve_class()
    {
        var instance = fixture().CreateInstance<SampleClass>();
        Assert(instance != null);
        Assert(instance.instance != null);
    }

    [Test]
    public void test_resolve_interface()
    {
        var instance = fixture().Resolve<IDepB>();
        Assert(instance != null);
        Assert(instance.A != null);
    }

    [Test]
    public void test_resolve_recursive()
    {
        var instance = fixture().Resolve<IDepC>();
        Assert(instance != null);
        Assert(instance.A != null);
        Assert(instance.B != null);
        Assert(instance.B.A == instance.A);
        Assert(instance.C == instance);
    }
}
#endif
