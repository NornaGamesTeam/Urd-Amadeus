using UnityEngine;
using Urd.View.Popup;

namespace Urd.Amadeus.Popup
{
    public class SkillTreePopupView : PopupView<SkillTreePopupModel>
    {
        [SerializeField]
        private PopupElementSkillTreeColumn _popupElementSkillTreeColumn;
        
        [SerializeField]
        private Transform _columnsParent;
    }
}