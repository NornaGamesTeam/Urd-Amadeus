using UnityEngine;

namespace Urd.Game.SkillTrees
{
    [System.Serializable]
    public class HitAreaCone : HitArea
    {
        public override HitAreaShape Shape => HitAreaShape.Cone;

        [field: SerializeField]
        public float AngleDegreesClockWise { get; private set; }
    }
}