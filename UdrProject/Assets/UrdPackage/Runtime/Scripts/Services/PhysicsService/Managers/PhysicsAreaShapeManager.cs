using UnityEngine;

namespace Urd.Services.Physics
{
    public abstract class PhysicsAreaShapeManager : IPhysicsAreaShapeManager
    {
        public abstract AreaShapeType AreaShape { get; }
        
        private DebugAreaShapeView _debugAreaShapeView;

        public abstract bool TryHit(Vector2 vector2, Vector2 originPoint, IHitModel hitModel);

        protected void PrintDebugObject(Vector2 originPoint, Vector2 direction, IHitModel hitModel)
        {
            if (_debugAreaShapeView == null
                || (_debugAreaShapeView?.OriginPoint != originPoint
                    || _debugAreaShapeView?.Direction != direction
                    || _debugAreaShapeView?.AreaShapeModel != hitModel.AreaShapeModel))
            {
                GameObject.Destroy(_debugAreaShapeView?.gameObject);
                CreateDebugObject(originPoint, direction, hitModel.AreaShapeModel);
            }
        }

        private void CreateDebugObject(Vector2 originPoint, Vector2 direction, IAreaShapeModel areaShapeModel)
        {
            var gameObject = new GameObject("DEBUG_CONE");
            gameObject.transform.position = originPoint;
            _debugAreaShapeView = gameObject.AddComponent<DebugAreaShapeView>();
            _debugAreaShapeView.SetAreaShapeModel(originPoint, direction, areaShapeModel);
        }
    }
}
