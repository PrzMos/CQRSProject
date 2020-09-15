using SharedKernel.DIContainers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedKernel.Dispatchers.Implementations
{
    public class SimpleEventPublisher : IDomainEventPublisher
    {
        protected IResolver _resolver;

        public SimpleEventPublisher(IResolver resolver)
        {
            this._resolver = resolver;
        }

        public void Publish<T>(T domainEvent) 
            where T : IDomainEvent
        {
            var _eventHandlers = this._resolver.ResolveAll<IEventListener<T>>();
            foreach (var handler in _eventHandlers)
            {
                handler.Handle(domainEvent);
            }
        }
    }
}
