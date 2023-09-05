using System.Collections.Generic;
using MyBox;
using UnityEngine;
using Urd.Utils;

namespace Urd.Services.Physics
{
    public class HitModel : IHitModel
    {
        public virtual LayerMaskTypes LayerMask { get; protected set; }

        public Vector2 Position { get; private set; }
        public Vector2 Direction { get; private set; }
        
        public IAreaShapeModel AreaShapeModel{ get; protected set; }
        public List<Collider2D> Collisions { get; protected set; } = new();

        public bool HasCollision => !Collisions.IsNullOrEmpty();
        public bool DrawDebug { get; set; }

        public HitModel(Vector2 position, Vector2 direction, IAreaShapeModel areaShapeModel,
            LayerMaskTypes layerMask)
        {
            Position = position;
            Direction = direction;
            AreaShapeModel = areaShapeModel;
            LayerMask = layerMask;
        }
        
        public void SetCollision(List<Collider2D> newCollisions)
        {
            Collisions = newCollisions;
        }

        public void AddCollision(Collider2D newCollider)
        {
            Collisions.Add(newCollider);
        }
    }
}
