using UnityEngine;

namespace Urd.UI.Dialog
{
    public class UIDialogViewConfig : ScriptableObject
    {
        [field:SerializeField]
        public float FadeInTime { get; private set; }

        [field:SerializeField]
        public float FadeOutTime { get; private set; }
        
        [field:SerializeField]
        public float ArrowFadeInTime { get; private set; }
        [field:SerializeField]
        public float ArrowFadeOutTime { get; private set; }
    }
}