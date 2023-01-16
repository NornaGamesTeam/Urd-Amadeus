using System;
using UnityEngine;
using Urd.Popup;
using Urd.Services.Navigation;

namespace Urd.View.Popup
{
    public class PopupView<T> : PopupViewNoModel, IPopupView where T : PopupModel
    {
        public GameObject GameObject => gameObject;
        public PopupModel PopupModel { get; private set; }
        public T Model => PopupModel as T;
        public event Action OnClickOnClose;

        public void SetPopupModel(PopupModel popupModel)
        {
            PopupModel = popupModel;


            popupModel.OnStatusChanged += OnPopupModelStatusChanged;
        }

        private void OnPopupModelStatusChanged(NavigableStatus statusFrom, NavigableStatus statusTo)
        {
            switch (statusTo)
            {
                case NavigableStatus.Opening: OnBeginOpen();
                    break;
                case NavigableStatus.Idle: OnIdle();
                    break;
                case NavigableStatus.Closing: OnBeginClose();
                    break;
                case NavigableStatus.Closed: OnClose();
                    break;
                case NavigableStatus.Destroyed: OnDestroy();
                    break;
            }
        }

        public virtual void Close()
        {
            OnClickOnClose?.Invoke();
        }
        protected virtual void OnBeginOpen() { }        
        protected virtual void OnIdle() { }
        protected virtual void OnBeginClose() { }
        protected virtual void OnClose() { }
        protected virtual void OnDestroy() { }
    }
}
