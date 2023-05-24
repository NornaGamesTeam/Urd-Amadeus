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
            
            var areaConeShapeModel = areaShapeModel as AreaShapeConeModel; 

            var targets = Physics2D.OverlapCircleAll(originPoint, areaConeShapeModel.Distance);
            
            for (int i = 0; i < targets.Length; i++)
            {
                Transform target = targets[i].transform;
                Vector3 dirToTarget = (target.position - (Vector3)originPoint).normalized;
                if (Vector3.Angle(direction, dirToTarget) < areaConeShapeModel.AngleDegreesClockWise * 0.5f)
                {
                    if (Physics2D.Raycast(originPoint, dirToTarget))
                    {
                        Debug.Log(target.name);
                    }
                }
            }
            
            return false;
        }
    }
}
