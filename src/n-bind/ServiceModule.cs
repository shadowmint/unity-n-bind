namespace N.Package.Bind
{
    /// Extend this class to bind services to the service registry
    public abstract class ServiceModule
    {
        /// Bind services here
        public abstract void Register(ServiceRegistry registry);
    }
}
