using System;
using System.Collections.Generic;
using UnityEngine;
using Urd.Utils;

namespace Urd.Game.Projectile
{
    [Serializable]
    public class ProjectileModel : IProjectileModel
    {
        [field: SerializeField] 
        public ProjectileView ProjectileView { get; private set; }

        [field: SerializeField]
        public LayerMaskTypes Objetive { get; private set; }
        
        [field: SerializeField]
        public float Speed { get; private set; }

        [field: SerializeField]
        public List<OffsetDirection<Vector3>> OffsetInitialPosition { get; protected set; }

        public bool HasDelayProjectile => DelayProjectile > 0;
        
        [field: SerializeField]
        public float DelayProjectile { get; private set; }

        public Vector3 Position { get; private set; }
        public Vector2 Direction { get; private set; }

        public event Action<Vector3> OnChangeDirection;
        public event Action<Vector3> OnChangePosition;
       public ProjectileModel(IProjectileModel projectileModel)
        {
            ProjectileView = projectileModel.ProjectileView;
            Objetive = projectileModel.Objetive;
            Speed = projectileModel.Speed;
            Position = projectileModel.Position;
            Direction = projectileModel.Direction;
            OffsetInitialPosition = projectileModel.OffsetInitialPosition;
        }        
        public void SetInitialPositionAndDirection(Vector3 position, Vector2 direction)
        {
            var directionType = DirectionUtils.ConvertToDirection(direction);
            var offsetPosition = OffsetInitialPosition?
                .Find(offsetInitialPosition 
                          => offsetInitialPosition.Direction == directionType)?.Class ?? Vector3.zero;
            Position = position + offsetPosition;
            Direction = direction;
            OnChangeDirection?.Invoke(Direction);
            OnChangePosition?.Invoke(Position);
        }
        
        public void Move(Vector3 movement)
        {
            Position += movement;
            OnChangePosition?.Invoke(Position);
        }
    }
}
