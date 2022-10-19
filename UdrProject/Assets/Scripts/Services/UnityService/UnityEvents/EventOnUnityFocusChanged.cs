using Urd.Services.EventBus;

namespace Urd.Services.Unity
{
    public class EventOnUnityFocusChanged : IEventBusMessage
    {
        public bool IsFocus{ get; private set; }

        public EventOnUnityFocusChanged(bool focus)
        {
            IsFocus = focus;
        }
    }
}