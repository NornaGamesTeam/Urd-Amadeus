using UnityEngine;
using Urd.Popup;
using Urd.Services;
using Urd.Utils;

namespace Urd.DebugTools
{
    public class DebugOpenPopup : DebugAbstract
    {
        public override void OnInputGetDown()
        {
            var popupInfoModel = new PopupInfoModel();
            StaticServiceLocator.Get<INavigationService>().Open(popupInfoModel, null);
        }

        private void OnOpenPopup(bool success)
        {
            Debug.Log($"Popup Opened {success}");
        }
    }
}