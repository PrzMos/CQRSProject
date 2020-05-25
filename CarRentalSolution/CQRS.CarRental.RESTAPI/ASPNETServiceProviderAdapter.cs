using SharedKernel.DIContainers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRS.CarRental.RESTAPI
{
    public class ASPNETServiceProviderAdapter : IResolver
    {
        private readonly IServiceProvider _serviceProvider;

        public ASPNETServiceProviderAdapter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public T Resolve<T>()
        {
            var result = (T)_serviceProvider.GetService(typeof(T));
            return result;
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            var result = (IEnumerable<T>)_serviceProvider.GetService(typeof(T));
            return result;
        }
    }
}
