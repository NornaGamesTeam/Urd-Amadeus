using System.Collections.Generic;
using UnityEngine;
using Urd.Popup;
using Urd.View.Popup;

namespace Urd.Services.Navigation
{
    public class PopupTypesConfig : ScriptableObject
    {
        [SerializeField]
        private List<PopupTypesConfigInfo> _popupList = new List<PopupTypesConfigInfo>();

        public bool Contains(INavigable navigable)
        {
            var popupModel = navigable as PopupModel;
            if (popupModel == null)
            {
                return false;
            }
            
            var popupType = popupModel.PopupType;
            return _popupList.Exists( popupInfo => popupInfo.PopupType == popupType);
        }
        
        public bool TryGetPopupView(PopupModel navigable, out IPopupView popupView)
        {
            popupView = _popupList.Find( popupInfo => popupInfo.PopupType == navigable.PopupType)?.PopupView;
            return popupView != null;
        }
    }

    [System.Serializable]
    internal class PopupTypesConfigInfo
    {
        [field: SerializeField]
        public PopupTypes PopupType { get; private set; }
        [field: SerializeField]
        public PopupViewNoModel PopupView { get; private set; }
    }
}