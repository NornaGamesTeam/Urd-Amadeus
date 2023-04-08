using System;
using System.Collections.Generic;
using Urd.Services.EventBus;

namespace Urd.Services
{
    [Serializable]
    public class EventBusService : BaseService, IEventBusService
    {
        private Dictionary<Type, IEventBusHandler> _observers = new Dictionary<Type, IEventBusHandler>();

        public override void Init()
        {
            base.Init();
            SetAsLoaded();
        }

        public void Subscribe(IEventBusObservableBase observer)
        {
            Subscribe(observer, GetMessageInterfaces(observer));
        }

        private Type[] GetMessageInterfaces(IEventBusObservableBase observer)
        {
            var messageTypes = new List<Type>();
            var interfaces = observer.GetType().GetInterfaces();
            for (int i = interfaces.Length - 1; i >= 0; i--)
            {
                var genericArguments = interfaces[i].GetGenericArguments();
                if (genericArguments != null && genericArguments.Length <= 0)
                {
                    continue;
                }

                messageTypes.Add(genericArguments[0]);
            }

            return messageTypes.ToArray();
        }

        public void Subscribe(IEventBusObservableBase observer, params Type[] messages)
        {
            for (int i = messages.Length - 1; i >= 0; i--)
            {
                var messageType = messages[i];

                if (!typeof(IEventBusMessage).IsAssignableFrom(messageType))
                {
                    continue;
                }

                if (_observers.TryGetValue(messageType, out var observerHandler))
                {
                    observerHandler.Subscribe(observer);

                    continue;
                }

                var generiEventBus = typeof(EventBusHandler<>).MakeGenericType(messageType);
                var ctor = generiEventBus.GetConstructors();
                var newObject = ctor[0].Invoke(new object[] { });
                var newEventBus = newObject as IEventBusHandler;
                newEventBus.Subscribe(observer);
                
                _observers.Add(messages[i], newEventBus);
            }
        }

        public void Unsubscribe(IEventBusObservableBase observer)
        {
            Unsubscribe(observer, GetMessageInterfaces(observer));
        }

        public void Unsubscribe(IEventBusObservableBase observer, params Type[] messages)
        {
            for (int i = messages.Length - 1; i >= 0; i--)
            {
                var messageType = messages[i];

                if (!typeof(IEventBusMessage).IsAssignableFrom(messageType))
                {
                    continue;
                }

                if (_observers.TryGetValue(messageType, out var observerHandler))
                {
                    observerHandler.Unsubscribe(observer);
                }
            }
        }

        public void Send(IEventBusMessage eventCalled)
        {
            if (_observers.TryGetValue(eventCalled.GetType(), out var eventBusHandler))
            {
                eventBusHandler.CallHandlers(eventCalled);
            }
        }
    }
}