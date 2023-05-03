using System;
using MyBox;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using Urd.Services;
using Urd.Utils;

namespace Urd.Character
{
    public class CharacterInput
    {
        private const string HORIZONTAL_MOVEMENT = "HorizontalMovement";
        private const string VERTICAL_MOVEMENT = "VerticalMovement";

        public Vector2 Movement => _movement;
        
        private Vector2 _movement;
        private IInputService _inputService;
        private CharacterModel _characterModel;
        
        public CharacterInput(CharacterModel characterModel)
        {
            _characterModel = characterModel;

            Init();
        }

        public void Init()
        {
            _inputService = StaticServiceLocator.Get<IInputService>();

            SetInput();
        }
        
        private void SetInput()
        {
            _inputService.SubscribeToActionOnPerformed(HORIZONTAL_MOVEMENT, OnHorizontalMovementDown);
            _inputService.SubscribeToActionOnCancel(HORIZONTAL_MOVEMENT, OnHorizontalMovementUp);
            
            _inputService.SubscribeToActionOnPerformed(VERTICAL_MOVEMENT, OnVerticalMovementDown);
            _inputService.SubscribeToActionOnCancel(VERTICAL_MOVEMENT, OnVerticalMovementUp);
        }

        private void OnHorizontalMovementDown(InputAction.CallbackContext inputAction)
        {
            _movement.x = inputAction.ReadValue<Single>();
            Debug.Log($"OnHorizontalMovementDown: {_movement.x}");
        }
        
        private void OnHorizontalMovementUp(InputAction.CallbackContext inputAction)
        {
            _movement.x = inputAction.ReadValue<Single>();
            Debug.Log($"OnHorizontalMovementUp: {_movement.x}");
        }
        
        private void OnVerticalMovementDown(InputAction.CallbackContext inputAction)
        {
            _movement.y = inputAction.ReadValue<Single>();
            Debug.Log($"OnVerticalMovementDown: {_movement.y}");
        }
        
        private void OnVerticalMovementUp(InputAction.CallbackContext inputAction)
        {
            _movement.y = inputAction.ReadValue<Single>();
            Debug.Log($"OnVerticalMovementUp: {_movement.y}");
        }
    }
}