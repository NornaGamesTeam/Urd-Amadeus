using System;
using System.Collections.Generic;
using UnityEngine;
using Urd.Character.Skill;
using Urd.Services.Physics;
using Urd.Utils;

namespace Urd.Game.Projectile
{
    public interface IProjectileModel : IDisposable
    {
        ProjectileView ProjectileViewPrefab { get; }
        LayerMaskTypes Objetive { get;  }
        float Speed { get;  }
        
        Vector3 Position { get; }
        Vector2 Direction { get; }
        List<OffsetDirectionParameter<Vector3>> OffsetInitialPosition { get; }
        List<OffsetDirectionReference<AreaShapeModel>> AreaShape { get; }

        bool HasDelayProjectile { get; }
        float DelayProjectile { get; }
        ElementType DamageElement { get; }
        float DamageFromStats { get; }

        public void SetInitialPositionAndDirection(Vector3 position, Vector2 direction);
        void Move(Vector3 movement);
        event Action<Vector3> OnChangePosition;
        event Action<Vector3> OnChangeDirection;
    }
}
