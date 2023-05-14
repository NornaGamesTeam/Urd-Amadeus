using System;
using System.Collections;
using System.Dynamic;
using MyBox;
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

        public Vector2 Movement => _movement;
        private Vector2 _movement;
        public bool IsDodging { get; private set; }

        private IInputService _inputService;
        private ICoroutineService _coroutineService;
        private CharacterModel _characterModel;
        private Coroutine _oneMovementCoroutine;
        private Vector2 _lastMovement;
        
        
        public event Action<Vector2> OnMovementChanged;
        public event Action<bool> OnIsDodgingChanged;

        public MainCharacterInput(CharacterModel characterModel)
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
            
            _inputService.SubscribeToActionOnPerformed(DODGE_SKILL, OnDodgeSkillDown);
            _inputService.SubscribeToActionOnCancel(DODGE_SKILL, OnDodgeSkillUp);

            _oneMovementCoroutine = _coroutineService.StartCoroutine(OneMovementCo());
        }

        private IEnumerator OneMovementCo()
        {
            while (true)
            {
                yield return new WaitForEndOfFrame();
                    OnMovementChanged?.Invoke(Movement);
                    _lastMovement = Movement;
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

            OnIsDodgingChanged = null;
            OnMovementChanged = null;
            
            _coroutineService.StopCoroutine(_oneMovementCoroutine);
        }

        private void OnHorizontalMovementDown(InputAction.CallbackContext inputAction) => _movement.x = inputAction.ReadValue<Single>();
        private void OnHorizontalMovementUp(InputAction.CallbackContext inputAction) => _movement.x = inputAction.ReadValue<Single>();
        private void OnVerticalMovementDown(InputAction.CallbackContext inputAction) => _movement.y = inputAction.ReadValue<Single>();
        private void OnVerticalMovementUp(InputAction.CallbackContext inputAction) => _movement.y = inputAction.ReadValue<Single>();
        
        private void OnDodgeSkillDown(InputAction.CallbackContext inputAction)
        {
            IsDodging = true;
            OnIsDodgingChanged?.Invoke(IsDodging);
        }
        private void OnDodgeSkillUp(InputAction.CallbackContext inputAction)
        {
            IsDodging = false;
            OnIsDodgingChanged?.Invoke(IsDodging);
        }
    }
}