using System.Collections.Generic;

namespace SharedKernel.DIContainers
{
    public interface IResolver
    {
        T Resolve<T>();
        IEnumerable<T> ResolveAll<T>();
    }
}
