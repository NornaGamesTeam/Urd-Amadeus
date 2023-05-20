using UnityEngine;

namespace Urd.Game.SkillTrees
{
    [CreateAssetMenu(fileName = "NewMeleeAttackSkill", menuName = "Urd/SkillTreeConfig/MeleeAttackSkill", order = 1)]
    public class MeleeAttackConfig : SkillConfig
    {
        [field: SerializeField]
        public float Distance { get; protected set; }
        
        [field: SerializeField]
        public float HitAngle { get; protected set; }
    }
}