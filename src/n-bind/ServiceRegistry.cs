using System;
using System.Collections.Generic;
using N.Package.Bind.Core;

namespace N.Package.Bind
{
  /// A basic service registry type that maps types to types.
  /// Instances are created on demand and cached locally.
  /// To discard a global or reset a binding use Clear()
  public class ServiceRegistry : IServiceRegistry
  {
    /// Bound type mappings
    private readonly IDictionary<Type, Type> _bindings = new Dictionary<Type, Type>();

    /// Bound instance mappings
    private readonly IDictionary<Type, object> _instances = new Dictionary<Type, object>();

    /// The property binder to use for this registry
    private readonly PropertyBinder _binder;

    /// Create a new instance
    public ServiceRegistry()
    {
      _binder = new PropertyBinder(this);
    }

    /// Register a set of services
    public void Register(IServiceModule module)
    {
      module.Register(this);
    }

    /// Register a single service binding
    public void Register<TInterface, TImpl>() where TImpl : TInterface
    {
      if (_bindings.ContainsKey(typeof(TInterface)))
      {
        throw ErrDuplicateBinding<TInterface, TImpl>();
      }

      _bindings[typeof(TInterface)] = typeof(TImpl);
    }

    /// Register a single service binding to a specific instance
    /// Notice this is a destructive function; binding an instance automatically
    /// runs the PropertyBinder on it to bind child values, regardless of their
    /// current values.
    public void Register<TInterface, TImpl>(TImpl instance) where TInterface : class where TImpl : class, TInterface
    {
      if (_bindings.ContainsKey(typeof(TInterface)))
      {
        throw ErrDuplicateBinding<TInterface>(instance);
      }

      if (instance == null)
      {
        throw ErrNullInstance<TInterface>();
      }

      _bindings[typeof(TInterface)] = instance.GetType();
      _instances[typeof(TInterface)] = instance;
      Bind(instance);
    }

    /// Register the type T as all of its implemented interfaces
    public void Register<TImpl>()
    {
      foreach (var interfaceType in typeof(TImpl).GetInterfaces())
      {
        if (_bindings.ContainsKey(interfaceType))
        {
          throw ErrDuplicateBinding(interfaceType, typeof(TImpl));
        }

        _bindings[interfaceType] = typeof(TImpl);
      }
    }

    /// Clear the binding on a type
    public void Clear<TInterface>()
    {
      if (_bindings.ContainsKey(typeof(TInterface)))
      {
        _bindings.Remove(typeof(TInterface));
      }
      if (_instances.ContainsKey(typeof(TInterface)))
      {
        _instances.Remove(typeof(TInterface));
      }
    }

    /// Reset everything
    public void Reset()
    {
      _bindings.Clear();
      _instances.Clear();
    }

    /// Reset instances only
    public void ResetInstances()
    {
      _instances.Clear();
    }

    /// Return the instance of T if possible
    /// @param T The type to resolve
    /// @return The instance of T or null
    public T Resolve<T>() where T : class
    {
      if (_bindings.ContainsKey(typeof(T)))
      {
        if (!_instances.ContainsKey(typeof(T)))
        {
          var targetType = _bindings[typeof(T)];
          var instance = CreateInstance(targetType);
          _instances[typeof(T)] = instance;
          Bind(instance);
        }
        return _instances[typeof(T)] as T;
      }
      return null;
    }

    /// Bind an instance of T
    public void Bind<T>(T instance) where T : class
    {
      _binder.Bind(instance);
    }

    /// Make a new instance of T
    private object CreateInstance(Type type)
    {
      object rtn;
      try
      {
        rtn = Activator.CreateInstance(type);
      }
      catch (Exception err)
      {
        throw ErrCreateFailed(type, err);
      }
      return rtn;
    }

    /// Make a new instance of T
    public T CreateInstance<T>() where T : class
    {
      var rtn = CreateInstance(typeof(T)) as T;
      Bind(rtn);
      return rtn;
    }

    /// Error factory: null instance
    private BindException ErrNullInstance<TInterface>()
    {
      return new BindException(BindError.NullBinding,
        "Cannot bind {0} to null object instance", typeof(TInterface));
    }

    /// Error factory: duplicate
    private static BindException ErrDuplicateBinding<TInterface>(TInterface instance)
    {
      return new BindException(BindError.DuplicateBinding,
        "Cannot bind {0} to {1}; {0} is already bound", typeof(TInterface), instance);
    }

    /// Error factory: duplicate
    private static BindException ErrDuplicateBinding<TInterface, TImpl>()
    {
      return new BindException(BindError.DuplicateBinding,
        "Cannot bind {0} to {1}; {0} is already bound", typeof(TInterface), typeof(TImpl));
    }

    /// Error factory: duplicate by type
    private static BindException ErrDuplicateBinding(Type interfaceType, Type implType)
    {
      return new BindException(BindError.DuplicateBinding,
        "Cannot bind {0} to {1}; {0} is already bound", interfaceType, implType);
    }

    /// Error factory: create failed
    private static BindException ErrCreateFailed(Type type, Exception exception)
    {
      return new BindException(BindError.CreateInstanceFailed,
          "Unable to create instance of {0}: {1}", type, exception.Message)
        {InnerException = exception};
    }
  }
}