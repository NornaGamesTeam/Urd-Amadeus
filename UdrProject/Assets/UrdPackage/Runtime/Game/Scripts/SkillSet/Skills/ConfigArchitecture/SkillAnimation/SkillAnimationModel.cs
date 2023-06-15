using System;
using UnityEngine;

namespace Urd.Game.SkillTrees
{
    [Serializable]
    public class SkillAnimationModel : ISkillAnimationModel
    {
        [field: SerializeField]
        public SkillAnimationNames AnimationName { get; private set; }

        [field: SerializeField]
        public int AnimationLoops { get; private set; } = 1;

        [field: SerializeField]
        public float Duration { get; private set; }
    }
}
