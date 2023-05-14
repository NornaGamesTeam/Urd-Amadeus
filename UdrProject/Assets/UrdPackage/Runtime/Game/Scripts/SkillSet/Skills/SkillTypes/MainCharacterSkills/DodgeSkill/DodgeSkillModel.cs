using Urd.Game.SkillTrees;

namespace Urd.Character.Skill
{
    public class DodgeSkillModel : SkillModel<DodgeSkillConfig>
    {
        public float Distance => _skillConfig.Distance;

        public DodgeSkillModel(DodgeSkillConfig skillConfig) : base(skillConfig) { }
    }
}