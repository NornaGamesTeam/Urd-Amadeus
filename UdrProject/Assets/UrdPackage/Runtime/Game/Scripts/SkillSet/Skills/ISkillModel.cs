using Urd.Game.SkillTrees;

namespace Urd.Character.Skill
{
    public interface ISkillModel
    {
        public string Name { get; }
        public int LevelToUnlock { get; }
        public SkillType Type { get; }
        public ISkillController Controller { get; }
        public bool IsActive { get; }
        public float Duration { get; }
        public CharacterAnimParameters AnimParameter { get; }
        void SetConfig(SkillConfig defaultSkillConfig);
        void SetIsActive(bool isActive);
    }
}