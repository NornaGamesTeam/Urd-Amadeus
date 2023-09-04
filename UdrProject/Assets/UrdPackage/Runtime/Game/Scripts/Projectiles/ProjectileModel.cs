using System;
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

        public Vector3 Position { get; private set; }
        public event Action<Vector3> OnChangePosition;

        public void SetPosition(Vector3 position)
        {
            Position = position;
        }
    }
}
