using UnityEngine;

namespace Urd.Game.SkillTrees
{
    public abstract class SkillAnimationModel : ISkillAnimationModel
    {
        [field: SerializeField]
        public SkillAnimationNames AnimationName { get; private set; }

        [field: SerializeField]
        public int AnimationLoops { get; private set; } = 1;
    }
}
