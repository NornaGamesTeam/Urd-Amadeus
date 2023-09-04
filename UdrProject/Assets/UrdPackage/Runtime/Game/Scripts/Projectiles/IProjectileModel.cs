using System;
using UnityEngine;
using Urd.Utils;

namespace Urd.Game.Projectile
{
    public interface IProjectileModel
    {
        ProjectileView ProjectileView { get; }
        LayerMaskTypes Objetive { get;  }
        float Speed { get;  }
        
        Vector3 Position { get; }
        Vector2 Direction { get; }


        void SetPosition(Vector3 position);
        event Action<Vector3> OnChangePosition;
    }
}
