using MyBox;
using UnityEngine;

namespace Urd.UI
{
    [CreateAssetMenu(fileName = "NewUICharacterConfig", menuName = "Urd/UI/UICharacterConfig", order = 1)]

    public class UICharacterConfig : ScriptableObject
    {
        [field: SerializeField, DisplayInspector]
        public UICharacterHitPointsConfig HitPointsConfig { get; private set; }
    }
}
