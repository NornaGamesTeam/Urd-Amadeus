using System;
using Urd.Game.SkillTrees;

namespace Urd.Character.Skill
{
    [Serializable]
    public class MeleeAttackModel : SkillModel<MeleeAttackConfig>
    {
        public float Distance => _skillConfig.Distance;
        public float HitAngle => _skillConfig.HitAngle;
    }
}