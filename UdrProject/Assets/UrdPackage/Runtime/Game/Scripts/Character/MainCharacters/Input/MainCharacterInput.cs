using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Urd.Inputs;
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
        private const string MELEE_ATTACK_BUTTON = "MeleeAttackButton";
        private const string RANGE_ATTACK_BUTTON = "RangeAttackButton";
        private const string RANGE_SWITCH_BUTTON = "RangeSwitchButton";
        
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
            
            _inputService.SubscribeToActionOnHold(MELEE_ATTACK_BUTTON, OnMeleeAttackButtonDown);
            _inputService.SubscribeToActionOnCancel(MELEE_ATTACK_BUTTON, OnMeleeAttackButtonUp);
            
            _inputService.SubscribeToActionOnHold(RANGE_ATTACK_BUTTON, OnRangeAttackButtonDown);
            _inputService.SubscribeToActionOnCancel(RANGE_ATTACK_BUTTON, OnRangeAttackButtonUp);
            
            _inputService.SubscribeToActionOnHold(RANGE_SWITCH_BUTTON, OnRangeSwitchButtonDown);
            _inputService.SubscribeToActionOnCancel(RANGE_SWITCH_BUTTON, OnRangeSwitchButtonUp);

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
            
            _inputService.UnsubscribeToActionOnPerformed(MELEE_ATTACK_BUTTON, OnMeleeAttackButtonDown);
            _inputService.UnsubscribeToActionOnCancel(MELEE_ATTACK_BUTTON, OnMeleeAttackButtonUp);
            
            _inputService.UnsubscribeToActionOnHold(RANGE_ATTACK_BUTTON, OnRangeAttackButtonDown);
            _inputService.UnsubscribeToActionOnCancel(RANGE_ATTACK_BUTTON, OnRangeAttackButtonUp);
            
            _inputService.UnsubscribeToActionOnHold(RANGE_SWITCH_BUTTON, OnRangeSwitchButtonDown);
            _inputService.UnsubscribeToActionOnCancel(RANGE_SWITCH_BUTTON, OnRangeSwitchButtonUp);

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
                _skillActionType = _isRangeSwitchPressed ?SkillActionType.Range:SkillActionType.Melee;
            Debug.Log($"OnGamePadAimDown : {_skillActionType}, _isRangeSwitchPressed:{_isRangeSwitchPressed}");
            }

        } 

        private void OnGamePadAimUp(InputAction.CallbackContext inputAction)
        {
            _aimDirection = Vector2.zero;
            _skillActionType = SkillActionType.None;
            Debug.Log($"OnGamePadAimUp : {_skillActionType}, _isRangeSwitchPressed:{_isRangeSwitchPressed}");
        }

        private void OnMeleeAttackButtonDown(InputAction.CallbackContext inputAction)
        {
            _skillActionType = SkillActionType.Melee;
        }
        
        private void OnMeleeAttackButtonUp(InputAction.CallbackContext inputAction)
        {
            _skillActionType = SkillActionType.None;
        }
        
        private void OnRangeAttackButtonDown(InputAction.CallbackContext inputAction)
        {
            _skillActionType = SkillActionType.Range;
        }
        
        private void OnRangeAttackButtonUp(InputAction.CallbackContext inputAction)
        {
            _skillActionType = SkillActionType.None;
        }
        
        private void OnRangeSwitchButtonDown(InputAction.CallbackContext inputAction)
        {
            _isRangeSwitchPressed = true;
            Debug.Log($"OnRangeSwitchButtonDown : {_skillActionType}, _isRangeSwitchPressed:{_isRangeSwitchPressed}");

        }
        
        private void OnRangeSwitchButtonUp(InputAction.CallbackContext inputAction)
        {
            _isRangeSwitchPressed = false;
            Debug.Log($"OnRangeSwitchButtonUp : {_skillActionType}, _isRangeSwitchPressed:{_isRangeSwitchPressed}");

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