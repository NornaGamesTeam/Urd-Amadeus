using System.Collections.Generic;
using MyBox;
using UnityEngine;
using Urd.Utils;

namespace Urd.Services.Physics
{
    public abstract class HitModel : IHitModel
    {
        public abstract LayerMaskTypes LayerMask { get; }
        public IAreaShapeModel AreaShapeModel{ get; protected set; }
        public List<Collider2D> Collisions { get; protected set; }

        public bool HasCollision => !Collisions.IsNullOrEmpty();

        protected HitModel(IAreaShapeModel areaShapeModel)
        {
            AreaShapeModel = areaShapeModel;
        }

        public void SetCollision(List<Collider2D> newCollisions)
        {
            Collisions = newCollisions;
        }
    }
}
