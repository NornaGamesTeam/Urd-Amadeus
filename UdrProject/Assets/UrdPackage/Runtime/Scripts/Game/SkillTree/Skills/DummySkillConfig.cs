using UnityEngine;

namespace Urd.Game.SkillTrees
{
    [CreateAssetMenu(fileName = "DummySkill", menuName = "Urd/SkillTreeConfig/DummySkill", order = 1)]
    [System.Serializable]
    public class DummySkillConfig : BaseSkillConfig
    {
        [field: SerializeField] public override SkillType Type { get; protected set; }
    }
}