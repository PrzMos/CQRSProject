namespace SharedKernel.DIContainers
{
    public interface IContainer
    {
        void Register<TSource, TDestination>()
            where TDestination : TSource;
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
