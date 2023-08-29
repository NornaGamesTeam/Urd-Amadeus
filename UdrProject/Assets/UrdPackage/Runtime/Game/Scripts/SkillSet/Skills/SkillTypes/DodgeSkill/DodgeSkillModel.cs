using System;
using UnityEngine;
using Urd.Game.SkillTrees;

namespace Urd.Character.Skill
{
    [Serializable]
    public class DodgeSkillModel : SkillModel<SkillConfig>
    {
        [field: SerializeField, Header("Specific Properties")]
        public float Distance { get; private set; }
    }
}