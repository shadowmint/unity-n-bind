namespace N.Package.Bind.Core
{
  /// The basic interface for resolving a type T into a specific instance I
  public interface IServiceRegistry
  {
    /// Return the instance of T if possible
    /// @param T The type to resolve
    /// @return The instance of T or null
    T Resolve<T>() where T : class;
  }
}