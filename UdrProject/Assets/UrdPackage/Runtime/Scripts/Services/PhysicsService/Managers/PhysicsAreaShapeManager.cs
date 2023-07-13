using UnityEngine;

namespace Urd.Services.Physics
{
    public abstract class PhysicsAreaShapeManager : IPhysicsAreaShapeManager
    {
        public abstract AreaShapeType AreaShape { get; }
        
        private DebugAreaShapeView _debugAreaShapeView;

        public abstract bool TryHit(IHitModel hitModel);

        private void Hit(Collider2D target)
        {
            
        }
        
        protected void PrintDebugObject(IHitModel hitModel)
        {
            Vector2 originPoint = hitModel.Position;
            Vector2 direction = hitModel.Direction;
                
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
            return;
            var gameObject = new GameObject($"DEBUG_{areaShapeModel.GetType()}");
            gameObject.transform.position = originPoint;
            _debugAreaShapeView = gameObject.AddComponent<DebugAreaShapeView>();
            _debugAreaShapeView.SetAreaShapeModel(originPoint, direction, areaShapeModel);
        }
    }
}
