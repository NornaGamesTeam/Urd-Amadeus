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
        public ElementType DamageElement { get; protected set; }
        [field: SerializeField, Range(0f,1f), Tooltip("Percentage of damage from the Stat")]
        public float DamageFromStats { get; protected set; }
        [field: SerializeField]
        public List<AttackAreaByDirection> DamageOverTime { get; protected set; }
    }
}