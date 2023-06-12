using DG.Tweening;
using UnityEngine;

namespace Urd.UI.Dialog
{
    [CreateAssetMenu(fileName = "New UIDialogConfig", menuName = "Urd/UI/UIDialogConfig", order = 1)]

    public class UIDialogConfig : ScriptableObject
    {
        [field: SerializeField]
        public float TimePerLetter { get; private set; }

        [field: SerializeField]
        public Ease Ease { get; private set; }
    }
}
