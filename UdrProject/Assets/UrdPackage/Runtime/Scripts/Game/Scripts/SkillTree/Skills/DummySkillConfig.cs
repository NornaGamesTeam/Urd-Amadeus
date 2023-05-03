using UnityEngine;
using Urd.Utils;

namespace Urd.Game.SkillTrees
{
    [CreateAssetMenu(fileName = "DummySkill", menuName = "Urd/SkillTreeConfig/DummySkill", order = 1)]
    [System.Serializable]
    public class DummySkillConfig : BaseSkillConfig
    {
        [field: SerializeField] 
        public override SkillType Type { get; protected set; }
        [field: SerializeField, PreviewSprite]
        public override Sprite Image { get; protected set; }
    }
}