using UnityEngine;
using Urd.Popup;
using Urd.Services;
using Urd.Utils;

namespace Urd.DebugTools
{
    public class DebugClosePopup : DebugAbstract
    {
        public override void OnInputGetDown()
        {
            var popupInfoModel = new PopupInfoModel();
            StaticServiceLocator.Get<INavigationService>().Close(popupInfoModel);
        }
    }
}