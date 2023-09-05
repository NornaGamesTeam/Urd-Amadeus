using System;
using MyBox;
using UnityEngine;
using Urd.Game.Projectile;
using Urd.Game.SkillTrees;

namespace Urd.Character.Skill
{
    [Serializable]
    public class RangeAttackModel : SkillModel<SkillConfig>
    {
        [field: SerializeField, DisplayInspector]
        public ProjectileConfig ProjectileConfig { get; protected set; }
    }
}