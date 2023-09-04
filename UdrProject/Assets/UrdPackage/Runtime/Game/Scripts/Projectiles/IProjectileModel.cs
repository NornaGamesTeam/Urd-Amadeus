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

        event Action<Vector3> OnChangePosition;
    }
}
