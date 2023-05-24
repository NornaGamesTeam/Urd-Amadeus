using MyBox.Internal;
using UnityEditor;
using UnityEngine;

namespace Urd.Services.Physics
{
    [System.Serializable]
    public class PhysicsAreaConeManager : PhysicsAreaShapeManager
    {
        public override AreaShapeType AreaShape => AreaShapeType.Cone;



        public override bool TryHit(Vector2 originPoint, Vector2 direction, IAreaShapeModel areaShapeModel)
        {
            PrintDebugObject(originPoint, direction, areaShapeModel);

            return false;
        }
    }
}
