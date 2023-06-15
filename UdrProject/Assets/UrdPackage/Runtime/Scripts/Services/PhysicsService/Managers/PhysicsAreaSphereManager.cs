using UnityEngine;
using Urd.Utils;

namespace Urd.Services.Physics
{
    [System.Serializable]
    public class PhysicsAreaSphereManager : PhysicsAreaShapeManager
    {
        public override AreaShapeType AreaShape => AreaShapeType.Sphere;

        public override bool TryHit(IHitModel hitModel)
        {
            Vector2 originPoint = hitModel.Position;
            Vector2 direction = hitModel.Direction;
            
            PrintDebugObject(hitModel);

            var areaShapeSphereModel = hitModel.AreaShapeModel as AreaShapeSphereModel;
            var targets = Physics2D.OverlapCircleAll(originPoint, areaShapeSphereModel.Radio, hitModel.LayerMask.ToLayer());
            for (int i = 0; i < targets.Length; i++)
            {
                hitModel.AddCollision(targets[i]);
            }

            return hitModel.Collisions.Count > 0;
        }
    }
}
