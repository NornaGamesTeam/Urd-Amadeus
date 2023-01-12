using UnityEngine;
using Urd.Popup;

namespace Urd.View.Popup
{
    public class PopupView<T> : PopupViewNoModel, IPopupView where T : PopupModel
    {
        public GameObject GameObject => gameObject;
        public T Model { get; private set; }
        
        public void SetPopupModel(PopupModel popupModel)
        {
            Model = popupModel as T;
        }

    }
}
