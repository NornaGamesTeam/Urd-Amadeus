using System;
using System.Collections;
using UnityEngine;
using Urd.Services;
using Urd.Utils;

namespace Urd.Character
{
    public class CharacterInput : ICharacterInput
    {   
        public Vector2 Movement => _movement;
        protected Vector2 _movement;
        private bool IsMoving => _movement != Vector2.zero;
        public Vector2 AimDirection => _aimDirection.normalized;
        protected Vector2 _aimDirection;
        private bool IsAiming => _aimDirection != Vector2.zero;

        protected bool _isAttacking;
        protected Vector2 _finalAimDirection;
        
        private ICharacterModel _characterModel;
        private ICoroutineService _coroutineService;
        private Coroutine _joinEventsCoroutine;

        public event Action<Vector2> OnMovementChanged;
        public event Action<Vector2> OnAimDirectionChanged;
        public event ICharacterInput.AttackDelegate OnAttackingChanged;

        public CharacterInput(ICharacterModel characterModel)
        {
            _characterModel = characterModel;

            Init();
        }
        
        public virtual void Init()
        {
            _coroutineService = StaticServiceLocator.Get<ICoroutineService>();
            _joinEventsCoroutine = _coroutineService.StartCoroutine(JoinMovementsCo());
        }
        
        public virtual void Dispose()
        {
            OnMovementChanged = null;
            _coroutineService.StopCoroutine(_joinEventsCoroutine);
        }

        private IEnumerator JoinMovementsCo()
        {
            while (true)
            {
                yield return new WaitForEndOfFrame();
                OnMovementChanged?.Invoke(Movement);

                if (IsAiming)
                {
                    _finalAimDirection = AimDirection;
                }

                if (!IsAiming && IsMoving)
                {
                    _finalAimDirection = Movement.normalized;
                }

                OnAimDirectionChanged?.Invoke(_finalAimDirection);
                OnAttackingChanged?.Invoke(_isAttacking, _finalAimDirection);
            }
        }
    }
}