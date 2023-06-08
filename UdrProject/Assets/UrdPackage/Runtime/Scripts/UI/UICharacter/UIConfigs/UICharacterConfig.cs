using UnityEngine;

namespace Urd.UI
{
    [CreateAssetMenu(fileName = "NewUICharacterConfig", menuName = "Urd/UI/UICharacterConfig", order = 1)]

    public class UICharacterConfig : ScriptableObject
    {
        [field: SerializeField]
        public UICharacterHitPointsConfig HitPointsConfig { get; private set; }
    }
}
