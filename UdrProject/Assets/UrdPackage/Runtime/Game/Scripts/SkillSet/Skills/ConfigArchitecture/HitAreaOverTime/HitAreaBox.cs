using UnityEngine;

namespace Urd.Game.SkillTrees
{
    [System.Serializable]
    public class HitAreaBox : HitArea
    {
        public override HitAreaShape Shape => HitAreaShape.Box;
        
        [field: SerializeField]
        public Vector2 Area { get; private set; }
    }
}