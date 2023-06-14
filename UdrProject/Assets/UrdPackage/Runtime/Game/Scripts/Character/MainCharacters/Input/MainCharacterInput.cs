using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Urd.Services;
using Urd.Utils;

namespace Urd.Character
{
    public class MainCharacterInput : ICharacterInput
    {
        private const string HORIZONTAL_MOVEMENT = "HorizontalMovement";
        private const string VERTICAL_MOVEMENT = "VerticalMovement";
        private const string DODGE_SKILL = "DodgeSkill";
        private const string GAMEPAD_MOVEMENT = "GamePadMovement";
        private const string GAMEPAD_AIM = "GamePadAim";

        public Vector2 Movement => _movement;
        private Vector2 _movement;

        public Vector2 AimDirection => _aimDirection.normalized;
        private Vector2 _aimDirection;
        public bool IsDodging { get; private set; }

        private IInputService _inputService;
        private ICoroutineService _coroutineService;
        private ICharacterModel _characterModel;
        private Coroutine _joinEventsCoroutine;

        private bool IsMoving => _movement != Vector2.zero;
        private bool IsAiming => _aimDirection != Vector2.zero;
        private bool _isAttacking;
        private Vector2 _finalAimDirection;

        public event Action<Vector2> OnMovementChanged;
        public event Action<Vector2> OnAimDirectionChanged;
        public event ICharacterInput.DodgeDelegate OnIsDodgingChanged;
        public event ICharacterInput.AttackDelegate OnAttackingChanged;
        public event ICharacterInput.InteractDelegate OnInteractDelegate;

        public MainCharacterInput(ICharacterModel characterModel)
        {
            _characterModel = characterModel;

            Init();
        }

        public void Init()
        {
            _inputService = StaticServiceLocator.Get<IInputService>();
            _coroutineService = StaticServiceLocator.Get<ICoroutineService>();

            SubscribeToInput();
        }

        private void SubscribeToInput()
        {
            _inputService.SubscribeToActionOnHold(HORIZONTAL_MOVEMENT, OnHorizontalMovementDown);
            _inputService.SubscribeToActionOnCancel(HORIZONTAL_MOVEMENT, OnHorizontalMovementUp);

            _inputService.SubscribeToActionOnHold(VERTICAL_MOVEMENT, OnVerticalMovementDown);
            _inputService.SubscribeToActionOnCancel(VERTICAL_MOVEMENT, OnVerticalMovementUp);

            _inputService.SubscribeToActionOnHold(DODGE_SKILL, OnDodgeSkillDown);
            _inputService.SubscribeToActionOnCancel(DODGE_SKILL, OnDodgeSkillUp);

            _inputService.SubscribeToActionOnHold(GAMEPAD_MOVEMENT, OnGamePadMovementDown);
            _inputService.SubscribeToActionOnCancel(GAMEPAD_MOVEMENT, OnGamePadMovementUp);
            _inputService.SubscribeToActionOnHold(GAMEPAD_AIM, OnGamePadAimDown);
            _inputService.SubscribeToActionOnCancel(GAMEPAD_AIM, OnGamePadAimUp);

            _joinEventsCoroutine = _coroutineService.StartCoroutine(JoinMovementsCo());
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

        public void Dispose()
        {
            UnsubscribeToInput();
        }

        private void UnsubscribeToInput()
        {
            _inputService.UnsubscribeToActionOnHold(HORIZONTAL_MOVEMENT, OnHorizontalMovementDown);
            _inputService.UnsubscribeToActionOnCancel(HORIZONTAL_MOVEMENT, OnHorizontalMovementUp);

            _inputService.UnsubscribeToActionOnHold(VERTICAL_MOVEMENT, OnVerticalMovementDown);
            _inputService.UnsubscribeToActionOnCancel(VERTICAL_MOVEMENT, OnVerticalMovementUp);

            _inputService.UnsubscribeToActionOnPerformed(DODGE_SKILL, OnDodgeSkillDown);
            _inputService.UnsubscribeToActionOnCancel(DODGE_SKILL, OnDodgeSkillUp);

            _inputService.UnsubscribeToActionOnHold(GAMEPAD_MOVEMENT, OnGamePadMovementDown);
            _inputService.UnsubscribeToActionOnCancel(GAMEPAD_MOVEMENT, OnGamePadMovementUp);
            _inputService.UnsubscribeToActionOnHold(GAMEPAD_AIM, OnGamePadAimDown);
            _inputService.UnsubscribeToActionOnCancel(GAMEPAD_AIM, OnGamePadAimUp);

            OnIsDodgingChanged = null;
            OnMovementChanged = null;

            _coroutineService.StopCoroutine(_joinEventsCoroutine);
        }

        private void OnHorizontalMovementDown(InputAction.CallbackContext inputAction) =>
            _movement.x = inputAction.ReadValue<Single>();

        private void OnHorizontalMovementUp(InputAction.CallbackContext inputAction) => _movement.x = 0;

        private void OnVerticalMovementDown(InputAction.CallbackContext inputAction) =>
            _movement.y = inputAction.ReadValue<Single>();

        private void OnVerticalMovementUp(InputAction.CallbackContext inputAction) => _movement.y = 0;

        private void OnGamePadMovementDown(InputAction.CallbackContext inputAction) =>
            _movement = inputAction.ReadValue<Vector2>();

        private void OnGamePadMovementUp(InputAction.CallbackContext inputAction) => _movement = Vector2.zero;

        private void OnGamePadAimDown(InputAction.CallbackContext inputAction)
        {
            var aimDirection = inputAction.ReadValue<Vector2>();
            if (aimDirection != Vector2.zero)
            {
                _aimDirection = aimDirection;
                _isAttacking = true;
            }
        }

        private void OnGamePadAimUp(InputAction.CallbackContext inputAction)
        {
            _aimDirection = Vector2.zero;
            _isAttacking = false;
        }

        private void OnDodgeSkillDown(InputAction.CallbackContext inputAction) =>
            OnIsDodgingChanged?.Invoke(inputAction.performed, _movement);
        private void OnDodgeSkillUp(InputAction.CallbackContext inputAction) =>
            OnIsDodgingChanged?.Invoke(false, _movement);
    }
}