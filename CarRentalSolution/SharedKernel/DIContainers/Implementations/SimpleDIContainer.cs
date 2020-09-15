using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SharedKernel.DIContainers.Implementations
{

    public class SimpleDIContainer : IContainer, IResolver
    {
        readonly List<KeyValuePair<Type, Type>> _types = new List<KeyValuePair<Type, Type>>();
        readonly List<KeyValuePair<Type, object>> _instances = new List<KeyValuePair<Type, object>>();

        public SimpleDIContainer()
        {
            // register itself as IResolver 
            this._types.Add(new KeyValuePair<Type, Type>(typeof(IResolver), this.GetType()));
            this._instances.Add(new KeyValuePair<Type, object>(typeof(IResolver), this));
        }

        public void Register<TSource, TDestination>()
            where TDestination : TSource
        {
            _types.Add(new KeyValuePair<Type, Type>(typeof(TSource), typeof(TDestination)));
        }

        public void Register<T>()
        {
            _types.Add(new KeyValuePair<Type, Type>(typeof(T), typeof(T)));
        }

        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            return new List<T>();
        }

        private object Resolve(Type t)
        {
            Type implementationType = null;
            implementationType = this._types.Find(x => x.Key == t).Value; 
            if (implementationType == null)
            {
                throw new Exception($"Nie zarejestrowano typu {t.Name}");
            }

            object instance = null;
            instance = this._instances.Find(x => x.Key == t).Value;
            if (instance != null)
            {
                return instance;
            }

            instance = CreateInstance(implementationType);
            this._instances.Add(new KeyValuePair<Type, object>(t, instance));

            return instance;
        }

        private object CreateInstance(Type implementationType)
        {
            ConstructorInfo[] ctors = implementationType.GetConstructors();
            if (ctors.Length == 0)
            {
                throw new Exception($"Typ {implementationType.Name} nie posiada konstruktorów");
            }
            if (ctors.Length > 1)
            {
                throw new Exception($"Typ {implementationType.Name} posiada więcej niż 1 konstruktor");
            }

            object[] ctorParams = ctors[0].GetParameters()
                .Select(x => Resolve(x.ParameterType))
                .ToArray();

            object instance = Activator.CreateInstance(implementationType, ctorParams);
            return instance;
        }
    }

    //public class SimpleDIContainer1 : IContainer
    //{
    //    readonly Dictionary<Type, Type> _types = new Dictionary<Type, Type>();
    //    readonly Dictionary<Type, object> _instances = new Dictionary<Type, object>();

    //    public SimpleDIContainer1()
    //    {
    //        // register itself as IResolver 
    //        this._types.Add(typeof(IResolver), this.GetType());
    //        this._instances.Add(typeof(IResolver), this);
    //    }

    //    public void Register<TSource, TDestination>()
    //        where TDestination : TSource
    //    {
    //        _types.Add(typeof(TSource), typeof(TDestination));
    //    }

    //    public void Register<T>()
    //    {
    //        _types.Add(typeof(T), typeof(T));
    //    }

    //    public T Resolve<T>()
    //    {
    //        return (T)Resolve(typeof(T));
    //    }

    //    private object Resolve(Type t)
    //    {
    //        Type implementationType;
    //        bool implementationFound = this._types.TryGetValue(t, out implementationType);
    //        if (implementationFound == false)
    //        {
    //            throw new Exception($"Nie zarejestrowano typu {t.Name}");
    //        }

    //        object instance;
    //        bool instanceFound = this._instances.TryGetValue(t, out instance);
    //        if (instanceFound == true)
    //        {
    //            return instance;
    //        }

    //        ConstructorInfo[] ctors = implementationType.GetConstructors();
    //        if (ctors.Length == 0)
    //        {
    //            throw new Exception($"Typ {implementationType.Name} nie posiada konstruktorów");
    //        }
    //        if (ctors.Length > 1)
    //        {
    //            throw new Exception($"Typ {implementationType.Name} posiada więcej niż 1 konstruktor");
    //        }

    //        object[] ctorParams = ctors[0].GetParameters()
    //            .Select(x => Resolve(x.ParameterType))
    //            .ToArray();

    //        instance = Activator.CreateInstance(implementationType, ctorParams);
    //        this._instances.Add(t, instance);

    //        return instance;
    //    }
    //}
}
