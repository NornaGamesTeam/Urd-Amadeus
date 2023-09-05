using System;
using System.Collections.Generic;
using UnityEngine;
using Urd.Character.Skill;
using Urd.Services.Physics;
using Urd.Utils;

namespace Urd.Game.Projectile
{
    [Serializable]
    public class ProjectileModel : IProjectileModel
    {
        [field: SerializeField] 
        public ProjectileView ProjectileViewPrefab { get; private set; }

        [field: SerializeField]
        public LayerMaskTypes Objetive { get; private set; }
        
        [field: SerializeField]
        public float Speed { get; private set; }

        public bool HasDelayProjectile => DelayProjectile > 0;
        
        [field: SerializeField]
        public float DelayProjectile { get; private set; }
        
        [field: SerializeField]
        public ElementType DamageElement { get; protected set; }
        [field: SerializeField, Range(0f,1f), Tooltip("Percentage of damage from the Stat")]
        public float DamageFromStats { get; protected set; }
        
        [field: SerializeField]
        public List<OffsetDirectionParameter<Vector3>> OffsetInitialPosition { get; protected set; }
        
        [field: SerializeField]
        public List<OffsetDirectionReference<AreaShapeModel>> AreaShape { get; protected set; }

        public Vector3 Position { get; private set; }
        public Vector2 Direction { get; private set; }

        public event Action<Vector3> OnChangeDirection;
        public event Action OnDestroy;
        
        public event Action<Vector3> OnChangePosition;
       public ProjectileModel(IProjectileModel projectileModel)
        {
            ProjectileViewPrefab = projectileModel.ProjectileViewPrefab;
            Objetive = projectileModel.Objetive;
            Speed = projectileModel.Speed;
            Position = projectileModel.Position;
            Direction = projectileModel.Direction;
            OffsetInitialPosition = projectileModel.OffsetInitialPosition;
            AreaShape = projectileModel.AreaShape;
            DamageElement = projectileModel.DamageElement;
            DamageFromStats = projectileModel.DamageFromStats;
        }        
        public void SetInitialPositionAndDirection(Vector3 position, Vector2 direction)
        {
            var directionType = DirectionUtils.ConvertToDirection(direction);
            var offsetPosition = OffsetInitialPosition?
                .Find(offsetInitialPosition 
                          => offsetInitialPosition.Direction == directionType)?.Item ?? Vector3.zero;
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

        public void Dispose()
        {
            
        }
    }
}
