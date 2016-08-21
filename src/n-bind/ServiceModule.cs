namespace N.Package.Bind
{
  /// Extend this class to bind services to the service registry
  public interface IServiceModule
  {
    /// Bind services here
    void Register(ServiceRegistry registry);
  }
}