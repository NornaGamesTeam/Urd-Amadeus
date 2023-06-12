using System.Collections.Generic;
using UnityEngine;
using Urd.UI.Dialog;

namespace Urd.Dialog
{
    public class DialogConfigs : ScriptableObject
    {
        [SerializeField] 
        private List<DialogConfigsTypes> _dialogConfig;

        public UIDialogConfig GetUIDialog(DialogTypes dialogType)
        {
            return _dialogConfig.Find(dialog => dialog.DialogType == dialogType)?.DialogConfig ?? GetUIDialog(DialogTypes.None);
        }

        [System.Serializable]
        public class DialogConfigsTypes
        {
            [field: SerializeField]
            public DialogTypes DialogType { get; private set; }
            [field: SerializeField]
            public UIDialogConfig DialogConfig { get; private set; }
        }
    }
}