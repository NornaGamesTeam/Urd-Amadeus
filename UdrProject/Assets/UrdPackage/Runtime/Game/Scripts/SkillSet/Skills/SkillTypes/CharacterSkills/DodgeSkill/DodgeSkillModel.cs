using System;
using Urd.Game.SkillTrees;

namespace Urd.Character.Skill
{
    [Serializable]
    public class DodgeSkillModel : SkillModel<DodgeSkillConfig>
    {
        public float Distance => _skillConfig.Distance;
    }
}