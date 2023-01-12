using UnityEngine;
using Urd.Popup;

namespace Urd.View.Popup
{
    public class PopupView<T> : PopupViewNoModel, IPopupView where T : PopupModel
    {
        public GameObject GameObject => gameObject;
        public PopupModel PopupModel { get; private set; }
        public T Model => PopupModel as T;

        public void SetPopupModel(PopupModel popupModel)
        {
            PopupModel = popupModel;
        }
    }
}
