using System.Collections.Generic;
using UnityEngine;

namespace Urd.Game.SkillTrees
{
    [CreateAssetMenu(fileName = "NewMeleeAttackSkill", menuName = "Urd/SkillTreeConfig/MeleeAttackSkill", order = 1)]
    public class MeleeAttackConfig : SkillConfig
    {
        [field: Header("Specific"), SerializeField]
        public float Distance { get; protected set; }
        
        [field: SerializeField, Range(0,360)]
        public float HitAngle { get; protected set; }
        
        [field: SerializeField]
        public float Damage { get; protected set; }
        [field: SerializeField]
        public List<HitAreaByDirection> DamageOverTime { get; protected set; }
    }
}