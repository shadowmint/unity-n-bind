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
        private IDictionary<Type, Type> bindings = new Dictionary<Type, Type>();

        /// Bound instance mappings
        private IDictionary<Type, object> instances = new Dictionary<Type, object>();

        /// The property binder to use for this registry
        private PropertyBinder binder;

        /// Create a new instance
        public ServiceRegistry()
        { binder = new PropertyBinder(this); }

        /// Register a set of services
        public void Register(IServiceModule module)
        { module.Register(this); }

        /// Register a single service binding
        public void Register<TInterface, TImpl>() where TImpl : TInterface
        {
            if (bindings.ContainsKey(typeof(TInterface)))
            { throw ErrDuplicateBinding<TInterface, TImpl>(); }

            bindings[typeof(TInterface)] = typeof(TImpl);
        }

        /// Register a single service binding to a specific instance
        /// Notice this is a destructive function; binding an instance automatically
        /// runs the PropertyBinder on it to bind child values, regardless of their
        /// current values.
        public void Register<TInterface, TImpl>(TImpl instance) where TInterface : class where TImpl : TInterface
        {
            if (bindings.ContainsKey(typeof(TInterface)))
            { throw ErrDuplicateBinding<TInterface>(instance); }

            if (instance == null)
            { throw ErrNullInstance<TInterface>();  }

            bindings[typeof(TInterface)] = instance.GetType();
            instances[typeof(TInterface)] = instance;
            Bind(instance);
        }

        /// Clear the binding on a type
        public void Clear<TInterface>()
        {
            if (bindings.ContainsKey(typeof(TInterface)))
            { bindings.Remove(typeof(TInterface)); }
            if (instances.ContainsKey(typeof(TInterface)))
            { instances.Remove(typeof(TInterface)); }
        }

        /// Reset everything
        public void Reset()
        {
            bindings.Clear();
            instances.Clear();
        }

        /// Reset instances only
        public void ResetInstances()
        { instances.Clear(); }

        /// Return the instance of T if possible
        /// @param T The type to resolve
        /// @return The instance of T or null
        public T Resolve<T>() where T : class
        {
            if (bindings.ContainsKey(typeof(T)))
            {
                if (!instances.ContainsKey(typeof(T)))
                {
                    var targetType = bindings[typeof(T)];
                    var instance = CreateInstance(targetType);
                    instances[typeof(T)] = instance;
                    Bind(instance);
                }
                return instances[typeof(T)] as T;
            }
            return null;
        }

        /// Bind an instance of T
        public void Bind<T>(T instance) where T : class
        { binder.Bind(instance); }

        /// Make a new instance of T
        private object CreateInstance(System.Type type)
        {
            object rtn = null;
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
            return new BindException(BindError.NULL_BINDING,
            "Cannot bind {0} to null object instance", typeof(TInterface));
        }

        /// Error factory: duplicate
        private BindException ErrDuplicateBinding<TInterface>(TInterface instance)
        {
            return new BindException(BindError.DUPLICATE_BINDING,
            "Cannot bind {0} to {1}; {0} is already bound", typeof(TInterface), instance);
        }

        /// Error factory: duplicate
        private BindException ErrDuplicateBinding<TInterface, TImpl>()
        {
            return new BindException(BindError.DUPLICATE_BINDING,
            "Cannot bind {0} to {1}; {0} is already bound", typeof(TInterface), typeof(TImpl));
        }

        /// Error factory: create failed
        private BindException ErrCreateFailed(System.Type type, Exception exception)
        {
            return new BindException(BindError.CREATE_INSTANCE_FAILED,
            "Unable to create instance of {0}: {1}", type, exception.Message)
            { innerException = exception };
        }
    }
}
