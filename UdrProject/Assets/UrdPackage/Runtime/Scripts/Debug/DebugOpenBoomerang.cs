using UnityEngine;
using Urd.Boomerang;
using Urd.Services;
using Urd.Utils;

namespace Urd.DebugTools
{
    public class DebugOpenBoomerang : DebugAbstract
    {
        public override void OnInputGetDown()
        {
            var popupInfoModel = new BoomerangInfoModel();
            StaticServiceLocator.Get<INavigationService>().Open(popupInfoModel, null);
        }

        private void OnOpenBoomerang(bool success)
        {
            Debug.Log($"Boomerang Opened {success}");
        }
    }
}