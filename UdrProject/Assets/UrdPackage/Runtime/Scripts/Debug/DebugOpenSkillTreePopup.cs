using UnityEngine;
using Urd.Amadeus.Popup;
using Urd.Popup;
using Urd.Services;
using Urd.Utils;

namespace Urd.DebugTools
{
    public class DebugOpenSkillTreePopup : DebugAbstract
    {
        public override void OnInputGetDown()
        {
            var skillTreePopupModel = new SkillTreePopupModel();
            StaticServiceLocator.Get<INavigationService>().Open(skillTreePopupModel, null);
        }

        private void OnOpenPopup(bool success)
        {
            Debug.Log($"Popup Opened {success}");
        }
    }
}