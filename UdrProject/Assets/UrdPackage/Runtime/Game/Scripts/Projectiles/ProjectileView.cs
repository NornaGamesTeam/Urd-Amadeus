using System;
using UnityEngine;

namespace Urd.Game.Projectile
{
    public class ProjectileView : MonoBehaviour, IProjectileView
    {
        private IProjectileModel _projectileModel;

        private Action _callbackOnImpact;
        public void SetUp(IProjectileModel projectileModel)
        {
            _projectileModel = projectileModel;
            _projectileModel.OnChangePosition += OnChangePosition;
        }

        public void Dispose()
        {
            _projectileModel.OnChangePosition -= OnChangePosition;
        }


        public void Begin(Action callbackOnImpact)
        {
            _callbackOnImpact = callbackOnImpact;
        }

        public void Finish()
        {
            Dispose();
        }
        
        private void OnChangePosition(Vector3 newPosition)
        {
            transform.position = newPosition;
        }
    }
}