using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Urd.Services.EventBus
{
    public interface IEventBusHandler
    {
        void CallHandlers(IEventBusMessage newEvent);
        
        void Subscribe(IEventBusObservableBase observer);
        void Unsubscribe(IEventBusObservableBase observer);
    }
}