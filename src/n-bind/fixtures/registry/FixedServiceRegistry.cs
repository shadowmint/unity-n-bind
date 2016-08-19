#if N_BIND_TESTS
using N.Package.Bind.Core;
using System.Collections.Generic;

public class FixedServiceRegistry : IServiceRegistry
{
  public Dictionary<System.Type, object> Bound = new Dictionary<System.Type, object>();

  public T Resolve<T>() where T : class
  {
    if (Bound.ContainsKey(typeof(T)))
    {
      return Bound[typeof(T)] as T;
    }
    return null;
  }
}
#endif