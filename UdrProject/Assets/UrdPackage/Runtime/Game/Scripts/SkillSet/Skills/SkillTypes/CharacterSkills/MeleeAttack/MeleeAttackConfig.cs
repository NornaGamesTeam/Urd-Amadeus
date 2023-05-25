using System.Collections.Generic;
using UnityEngine;

namespace Urd.Game.SkillTrees
{
    [CreateAssetMenu(fileName = "NewMeleeAttackSkill", menuName = "Urd/SkillTreeConfig/MeleeAttackSkill", order = 1)]
    public class MeleeAttackConfig : SkillConfig
    {
        [field: SerializeField]
        public float Damage { get; protected set; }
        [field: SerializeField]
        public List<AttackAreaByDirection> DamageOverTime { get; protected set; }
    }
}