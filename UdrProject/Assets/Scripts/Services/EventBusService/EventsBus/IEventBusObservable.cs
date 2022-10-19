using Urd.Services.EventBus;

public interface IEventBusObservable<T> : IEventBusObservableBase where T : IEventBusMessage 
{
    void OnNewEvent(T newEvent);
}