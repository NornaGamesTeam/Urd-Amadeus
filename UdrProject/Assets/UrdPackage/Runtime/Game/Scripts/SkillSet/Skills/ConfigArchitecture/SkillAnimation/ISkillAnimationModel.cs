namespace Urd.Game.SkillTrees
{
    public interface ISkillAnimationModel
    {
        SkillAnimationNames AnimationName { get; }
        int AnimationLoops { get; }
        float Duration { get; }
    }
}