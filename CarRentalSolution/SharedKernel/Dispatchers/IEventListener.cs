using System;
using System.Collections.Generic;
using System.Text;

namespace SharedKernel.Dispatchers
{
    public interface IEventListener
    { }

    public interface IEventListener<TEvent> : IEventListener 
        where TEvent : IDomainEvent
    {
        void Handle(TEvent eventData);
    }
}
