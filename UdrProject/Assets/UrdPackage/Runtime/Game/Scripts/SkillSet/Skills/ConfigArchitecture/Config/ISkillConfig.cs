using Urd.Character.Skill;

namespace Urd.Game.SkillTrees
{
    public interface ISkillConfig
    {
        public ISkillController Controller { get; }
        public ISkillModel Model { get;  }
    }
}
