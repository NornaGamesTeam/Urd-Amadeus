using System.Collections.Generic;
using UnityEngine;
using Urd.Utils;

namespace Urd.Services.Physics
{
    public interface IHitModel
    {
        public Vector2 Position { get; }
        public Vector2 Direction { get; }
        
        LayerMaskTypes LayerMask { get; }

        IAreaShapeModel AreaShapeModel{ get; }

        List<Collider2D> Collisions { get;  }
        
        bool HasCollision { get; }

        void SetCollision(List<Collider2D> newCollisions);
        
        void AddCollision(Collider2D newCollider);
    }
}
