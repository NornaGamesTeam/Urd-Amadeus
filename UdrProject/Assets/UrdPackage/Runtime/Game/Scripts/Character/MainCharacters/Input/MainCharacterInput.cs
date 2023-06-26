using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Urd.Services;
using Urd.Utils;

namespace Urd.Character
{
    public class MainCharacterInput : CharacterInput
    {
        private const string HORIZONTAL_MOVEMENT = "HorizontalMovement";
        private const string VERTICAL_MOVEMENT = "VerticalMovement";
        private const string DODGE_SKILL = "DodgeSkill";
        private const string GAMEPAD_MOVEMENT = "GamePadMovement";
        private const string GAMEPAD_AIM = "GamePadAim";
        private const string INTERACT_BUTTON = "InteractButton";
        
        private IInputService _inputService;
        
        public bool IsDodging { get; private set; }
        public delegate void DodgeDelegate(bool isDodging, Vector2 dodgeDirection);
        public event DodgeDelegate OnIsDodgingChanged;
        public delegate void InteractDelegate(bool isInteracting, Vector2 interactDirection);
        public event InteractDelegate OnInteractChanged;

        public MainCharacterInput(ICharacterModel characterModel): base(characterModel) { }

        public override void Init()
        {
            base.Init();
            
            _inputService = StaticServiceLocator.Get<IInputService>();

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

            _inputService.SubscribeToActionOnPerformed(INTERACT_BUTTON, OnInteractDown);
            _inputService.SubscribeToActionOnCancel(INTERACT_BUTTON, OnInteractUp);
        }
        
        public override void Dispose()
        {
            base.Dispose();
            
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

            _inputService.UnsubscribeToActionOnPerformed(INTERACT_BUTTON, OnInteractDown);
            _inputService.UnsubscribeToActionOnCancel(INTERACT_BUTTON, OnInteractUp);
            
            OnIsDodgingChanged = null;
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

        private void OnInteractDown(InputAction.CallbackContext inputAction) =>
            OnInteractChanged?.Invoke(inputAction.performed, _finalAimDirection);
        private void OnInteractUp(InputAction.CallbackContext inputAction)=>
            OnInteractChanged?.Invoke(false, _finalAimDirection);
    }
}