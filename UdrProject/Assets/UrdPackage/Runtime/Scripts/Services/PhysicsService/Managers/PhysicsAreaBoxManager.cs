using UnityEngine;
using Urd.Utils;

namespace Urd.Services.Physics
{
    [System.Serializable]
    public class PhysicsAreaBoxManager : PhysicsAreaShapeManager
    {
        public override AreaShapeType AreaShape => AreaShapeType.Box;

        public override bool TryHit(IHitModel hitModel)
        {
            Vector2 originPoint = hitModel.Position;
            Vector2 direction = hitModel.Direction;
            
            PrintDebugObject(hitModel);

            var areaShapeBoxModel = hitModel.AreaShapeModel as AreaShapeBoxModel;

            originPoint += new Vector2(areaShapeBoxModel.Area.x * direction.x*0.5f , areaShapeBoxModel.Area.y * direction.y*0.5f); 
            var targets = Physics2D.OverlapBoxAll(originPoint, areaShapeBoxModel.Area,
                                                  Vector3.Angle(Vector2.up, direction), hitModel.LayerMask.ToLayer());
            for (int i = 0; i < targets.Length; i++)
            {
                hitModel.AddCollision(targets[i]);
            }

            return hitModel.Collisions.Count > 0;
        }
    }
}
