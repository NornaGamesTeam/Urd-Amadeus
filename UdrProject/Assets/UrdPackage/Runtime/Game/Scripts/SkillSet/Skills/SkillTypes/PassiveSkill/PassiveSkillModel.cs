using System;
using Urd.Game.SkillTrees;

namespace Urd.Character.Skill
{
    [Serializable]
    public abstract class PassiveSkillModel : SkillModel<SkillConfig>
    {
        public override SkillType Type => SkillType.Pasive;
    }
}