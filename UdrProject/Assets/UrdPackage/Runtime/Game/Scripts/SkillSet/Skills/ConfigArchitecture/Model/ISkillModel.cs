using Urd.Game.SkillTrees;
using Urd.Timer;
using Urd.UI;

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
        float CoolDown { get; }
        ISkillAnimationModel SkillAnimationModel { get; }
        TimerModel TimerModel { get; }
        UISkillConfig UISkillConfig { get; }
        void SetConfig(SkillConfig defaultSkillConfig);
        void SetIsActive(bool isActive);
    }
}