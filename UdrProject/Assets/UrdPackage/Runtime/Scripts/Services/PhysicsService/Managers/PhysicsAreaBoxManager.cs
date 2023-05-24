using UnityEngine;

namespace Urd.Services.Physics
{
    [System.Serializable]
    public class PhysicsAreaBoxManager : PhysicsAreaShapeManager
    {
        public override AreaShapeType AreaShape => AreaShapeType.Box;
        public override bool TryHit(Vector2 originPoint, Vector2 direction, IAreaShapeModel areaShapeModel)
        {
            PrintDebugObject(originPoint, direction, areaShapeModel);
            
            return false;
        }
    }
}
