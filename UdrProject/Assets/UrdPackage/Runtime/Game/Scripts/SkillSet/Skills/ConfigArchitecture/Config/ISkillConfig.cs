namespace Urd.Game.SkillTrees
{
    public interface ISkillConfig
    {
        SkillType Type { get; }
        float Duration { get; }
        ISkillAnimationModel SkillAnimationModel { get; }
    }
}
