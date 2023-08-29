
using System;
using UnityEngine;
using Urd.Game.SkillTrees;

namespace Urd.Character.Skill
{
    [Serializable]
    public class PassiveSkillModel : SkillModel<SkillConfig>
    {
        [field: SerializeField, Header("Specific properties")]
        public StatType AffectedElement { get; protected set; }
        [field: SerializeField]
        public float Factor { get; protected set; }
    }
}