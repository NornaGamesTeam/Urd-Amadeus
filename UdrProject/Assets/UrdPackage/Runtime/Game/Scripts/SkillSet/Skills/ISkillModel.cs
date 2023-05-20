using Urd.Game.SkillTrees;
using Urd.Timer;

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
        public TimerModel TimerModel { get; }
        void SetConfig(SkillConfig defaultSkillConfig);
        void SetIsActive(bool isActive);
    }
}