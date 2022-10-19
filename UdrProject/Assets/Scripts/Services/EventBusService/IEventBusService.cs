using System;
using Urd.Services.EventBus;

namespace Urd.Services
{
    public interface IEventBusService
    {
        void Subscribe(IEventBusObservableBase observer);
        void Subscribe(IEventBusObservableBase observer, params Type[] messages);
        void Unsubscribe(IEventBusObservableBase observer);
        void Unsubscribe(IEventBusObservableBase observer, params Type[] messages);
        void Call(IEventBusMessage eventCalled);
    }
}