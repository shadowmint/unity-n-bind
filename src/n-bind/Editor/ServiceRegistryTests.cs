#if N_BIND_TESTS
using NUnit.Framework;
using N.Package.Bind;
using N;

public class TestServiceModule : ServiceModule
{
    public override void Register(ServiceRegistry registry)
    {
        registry.Register<IDepA, ImplA>();
    }
}

public class ServiceRegistryTests : N.Tests.Test
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
            Assert(err.errorCode == BindError.DUPLICATE_BINDING);
        }
    }

    [Test]
    public void test_manual_register_gameobject()
    {
        var registry = new ServiceRegistry();
        var rp = this.SpawnComponent<ImplAComponent>();
        registry.Register<IDepA, ImplAComponent>(rp);
        this.TearDown();
    }

    [Test]
    public void test_service_module_registration()
    {
        var registry = new ServiceRegistry();
        registry.Register(new TestServiceModule());
    }
}
#endif
