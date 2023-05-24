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
            
            var areaShapeBoxModel = areaShapeModel as AreaShapeBoxModel;
            
            var targets = Physics2D.OverlapBoxAll(originPoint, areaShapeBoxModel.Area, Vector3.Angle(Vector2.up, direction));
            for (int i = 0; i < targets.Length; i++)
            {
                Debug.Log(targets[i]);
            }
            return false;
        }
    }
}
