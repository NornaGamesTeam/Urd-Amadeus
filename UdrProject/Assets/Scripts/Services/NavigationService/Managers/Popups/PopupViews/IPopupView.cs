using System;
using UnityEngine;
using Urd.Popup;

namespace Urd.View.Popup
{
    public interface IPopupView
    {
        PopupModel PopupModel { get; }
        void SetPopupModel(PopupModel popupModel);
        GameObject GameObject { get; }
        event Action OnClickOnClose;
        void Close();
    }
}