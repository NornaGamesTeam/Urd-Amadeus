using UnityEngine;

namespace Urd.UI
{
    [CreateAssetMenu(fileName = "NewUICharacterHitPointsConfig", menuName = "Urd/UI/UIHitPointsConfig", order = 1)]
    public class UICharacterHitPointsConfig : ScriptableObject
    {
        [field: SerializeField]
        public Color BackgroundColor { get; private set; }
        [field: SerializeField]
        public Color FillColor { get; private set; }
        [field: SerializeField]
        public Color TextColor { get; set; }
    }
}
