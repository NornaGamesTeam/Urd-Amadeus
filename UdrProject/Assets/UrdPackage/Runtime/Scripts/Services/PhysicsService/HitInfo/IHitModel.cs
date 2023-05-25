using System.Collections.Generic;
using UnityEngine;
using Urd.Utils;

namespace Urd.Services.Physics
{
    public interface IHitModel
    {
        LayerMaskTypes LayerMask { get; }

        IAreaShapeModel AreaShapeModel{ get; }

        List<Collider2D> Collisions { get;  }
        
        bool HasCollision { get; }

        void SetCollision(List<Collider2D> newCollisions);
    }
}
