using System;
using UnityEngine;

namespace Urd.Character.Skill
{
    [Serializable]
    public class ChangeStatsPassiveSkillModel : PassiveSkillModel
    {
        [field: SerializeField, Header("Specific properties")]
        public StatType Stat { get; protected set; }

        [field: SerializeField, Range(-1f,1f)]
        public float Factor { get; protected set; }
    }
}