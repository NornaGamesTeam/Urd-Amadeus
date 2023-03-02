using Urd.Services.EventBus;

namespace Urd.Services.Unity
{
    public class EventOnUnityPausedChanged : IEventBusMessage
    {
        public bool IsPaused { get; private set; }

        public EventOnUnityPausedChanged(bool paused)
        {
            IsPaused = paused;
        }
    }
}