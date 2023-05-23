using UnityEditor;
using UnityEngine;

namespace Urd.Services.Physics
{
    [System.Serializable]
    public class PhysicsAreaConeManager : PhysicsAreaShapeManager
    {
        private GameObject _debug;
        public override AreaShapeType AreaShape => AreaShapeType.Cone;
        public override bool TryHit(Vector2 originPoint, Vector2 direction, IAreaShapeModel areaShapeModel)
        {
            if (_debug != null)
            {
                GameObject.Destroy(_debug);
            }
            
            _debug = new GameObject("DEBUG_CONE");
            _debug.transform.position = originPoint;
            var debugAreaShapeView = _debug.AddComponent<DebugAreaShapeView>();
            debugAreaShapeView.SetAreaShapeModel(originPoint, direction, areaShapeModel);
            
            return false;
        }
    }
}
