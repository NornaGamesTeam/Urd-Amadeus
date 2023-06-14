using System;
using System.Collections.Generic;
using UnityEngine;
using Urd.Game.SkillTrees;

namespace Urd.Character.Skill
{
    [Serializable]
    public class MeleeAttackModel : SkillModel<SkillConfig>
    {
        [field: SerializeField, Header("Specific properties")]
        public float Damage { get; protected set; }
        [field: SerializeField]
        public List<AttackAreaByDirection> DamageOverTime { get; protected set; }
    }
}