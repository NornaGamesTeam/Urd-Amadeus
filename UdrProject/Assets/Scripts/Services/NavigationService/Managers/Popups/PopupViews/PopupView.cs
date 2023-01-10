using Urd.Popup;

namespace Urd.View.Popup
{
    public class PopupView<T> : PopupViewNoModel where T : PopupModel
    {
        public T Model { get; private set; }
        
        
    }
}
