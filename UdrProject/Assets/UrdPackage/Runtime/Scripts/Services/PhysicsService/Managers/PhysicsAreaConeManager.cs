using UnityEngine;
using Urd.Utils;

namespace Urd.Services.Physics
{
    [System.Serializable]
    public class PhysicsAreaConeManager : PhysicsAreaShapeManager
    {
        public override AreaShapeType AreaShape => AreaShapeType.Cone;
        public override bool TryHit(IHitModel hitModel)
        {
            Vector2 originPoint = hitModel.Position;
            Vector2 direction = hitModel.Direction;
            
            PrintDebugObject(hitModel);
            
            var areaConeShapeModel = hitModel.AreaShapeModel as AreaShapeConeModel; 

            var targets = Physics2D.OverlapCircleAll(originPoint, areaConeShapeModel.Distance,
                hitModel.LayerMask.ToLayer());
            
            for (int i = 0; i < targets.Length; i++)
            {
                Transform target = targets[i].transform;
                Vector3 dirToTarget = (target.position - (Vector3)originPoint).normalized;
                if (Vector3.Angle(direction, dirToTarget) < areaConeShapeModel.AngleDegreesClockWise * 0.5f)
                {
                    var raycastHit2D = Physics2D.Raycast(originPoint, dirToTarget, areaConeShapeModel.Distance, 
                                                         hitModel.LayerMask.ToLayer());
                    if (raycastHit2D.collider != null)
                    {
                        hitModel.AddCollision(targets[i]);
                    }
                }
            }
            
            return hitModel.Collisions.Count > 0;
        }
    }
}
