using Urd.Game.SkillTrees;

namespace Urd.Character.Skill
{
    public interface ISkillModel
    {
        string Name { get; }
        int LevelToUnlock { get; }
        SkillType Type { get; }
        ISkillController Controller { get; }
        bool IsActive { get; }
        float Duration { get; }
        string AnimatorName { get; }

        bool IsInCoolDown { get; }

        void SetConfig(SkillConfig defaultSkillConfig);
        void SetIsActive(bool isActive);
    }
}