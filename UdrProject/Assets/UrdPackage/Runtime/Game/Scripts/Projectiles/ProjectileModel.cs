using System;
using UnityEngine;
using Urd.Services.Physics;
using Urd.Utils;

namespace Urd.Game.Projectile
{
    [Serializable]
    public class ProjectileModel : IProjectileModel
    {
        [field: SerializeField] 
        public ProjectileView ProjectileView;

        [field: SerializeField]
        public LayerMaskTypes Objetive { get; private set; }
        
        [field: SerializeField]
        public float Speed { get; private set; }
    }
}
