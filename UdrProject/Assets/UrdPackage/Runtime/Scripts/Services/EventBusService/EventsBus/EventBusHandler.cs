using System.Collections;
using System.Collections.Generic;

namespace Urd.Services.EventBus
{
    public class EventBusHandler<TEventBusMessage> : IEventBusHandler where TEventBusMessage : class, IEventBusMessage
    {
        private List<IEventBusObservable<TEventBusMessage>> _observers = new List<IEventBusObservable<TEventBusMessage>>();

        public void CallHandlers(EventBus.IEventBusMessage newEvent)
        {
            for (int i = 0; i < _observers.Count; i++)
            {
                _observers[i].OnNewEvent(newEvent as TEventBusMessage);
            }
        }

        //public void Subscribe(IEventBusObservable<TEventBusMessage> observer)
        public void Subscribe(IEventBusObservableBase newObserver)
        {
            var observer = newObserver as IEventBusObservable<TEventBusMessage>;
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
        }

        public void Unsubscribe(IEventBusObservableBase observerToSubscribe)
        {
            _observers.Remove(observerToSubscribe as IEventBusObservable<TEventBusMessage>);
        }
    }
}