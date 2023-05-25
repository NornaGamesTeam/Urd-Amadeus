using System;
using System.Collections.Generic;
using Urd.Game.SkillTrees;

namespace Urd.Character.Skill
{
    [Serializable]
    public class MeleeAttackModel : SkillModel<MeleeAttackConfig>
    {
        public float Damage => _skillConfig.Damage;
        public List<AttackAreaByDirection> DamageOverTime => _skillConfig.DamageOverTime;
    }
}