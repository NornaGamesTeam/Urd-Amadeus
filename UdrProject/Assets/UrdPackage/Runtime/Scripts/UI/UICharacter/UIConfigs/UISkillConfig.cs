using DG.Tweening;
using UnityEngine;
using Urd.Utils;

namespace Urd.UI
{
    [CreateAssetMenu(fileName = "NewDodgeSkill", menuName = "Urd/UI/UISkillConfig", order = 1)]

    public class UISkillConfig : ScriptableObject
    {
        [field: SerializeField, Header("Enable Colors")]
        public Color BackgroundColor { get; private set; }
        [field: SerializeField, PreviewSprite]
        public Sprite BackgroundImage { get; private set; }
        [field: SerializeField]
        public Color ImageColor { get; private set; }
        [field: SerializeField, PreviewSprite]
        public Sprite Image { get; private set; }
        
        [field: SerializeField, Header("CoolDown Colors")]
        public Color BackgroundColorCoolDown { get; private set; }
        [field: SerializeField]
        public Color ImageColorCoolDown { get; private set; }
        [field: SerializeField]
        public Ease AnimationEase { get; private set; }
    }
}
