using System;
using Unity.Collections;
using UnityEngine;

namespace Urd.Game.Projectile
{
    public class ProjectileView : MonoBehaviour, IProjectileView
    {
        private const string ANIMATION_KEY_AIM_X = "AIM_X";
        private const string ANIMATION_KEY_AIM_Y = "AIM_Y";
        
        [SerializeReference, ReadOnly]
        private IProjectileModel _projectileModel;

        private Action _callbackOnImpact;
        
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetUp(IProjectileModel projectileModel)
        {
            _projectileModel = projectileModel;
            _projectileModel.OnChangePosition += OnChangePosition;
            _projectileModel.OnChangeDirection += OnChangeDirection;
        }

        private void OnDestroy()
        {
            _projectileModel.OnChangePosition -= OnChangePosition;
            _projectileModel.OnChangeDirection -= OnChangeDirection;
        }

        private void OnChangeDirection(Vector3 direction)
        {
            _animator.SetFloat(ANIMATION_KEY_AIM_X, direction.x);
            _animator.SetFloat(ANIMATION_KEY_AIM_Y, direction.y);
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