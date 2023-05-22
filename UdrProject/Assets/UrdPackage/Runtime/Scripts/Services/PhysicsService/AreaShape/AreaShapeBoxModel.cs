using UnityEngine;
using Urd.Services.Physics;

namespace Urd.Game.SkillTrees
{
    [System.Serializable]
    public class AreaShapeBoxModel : AreaShapeModel
    {
        public override AreaShapeType AreaShape => AreaShapeType.Box;
        
        [field: SerializeField]
        public Vector2 Area { get; private set; }

    }
}