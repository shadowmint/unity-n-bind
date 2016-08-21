using System.Reflection;

namespace N.Package.Bind.Core
{
  /// Given a service registry R and an instance of type T, bind every
  /// attribute on the instance to the know binding in the registry.
  public class PropertyBinder
  {
    /// The service registry
    private IServiceRegistry registry;

    /// Service factory method
    private readonly MethodInfo _factory;

    /// Create a new instance
    /// @param registry The service registry to use
    public PropertyBinder(IServiceRegistry registry)
    {
      this.registry = registry;
      _factory = registry.GetType().GetMethod("Resolve");
    }

    /// Bind an instance of T
    public void Bind<T>(T instance) where T : class
    {
      foreach (var prop in instance.GetType().GetSetProperties())
      {
        var type = prop.PropertyType;
        if (type.IsValueType) continue;

        var factory = this._factory.MakeGenericMethod(type);
        var resolved = factory.Invoke(registry, null);
        if (resolved != null)
        {
          prop.SetValue(instance, resolved, null);
        }
      }
    }
  }
}