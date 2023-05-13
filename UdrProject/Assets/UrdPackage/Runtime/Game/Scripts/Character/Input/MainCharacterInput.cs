using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Urd.Services;
using Urd.Utils;

namespace Urd.Character
{
    public class MainCharacterInput
    {
        private const string HORIZONTAL_MOVEMENT = "HorizontalMovement";
        private const string VERTICAL_MOVEMENT = "VerticalMovement";

        public Vector2 Movement => _movement;
        
        private Vector2 _movement;
        private IInputService _inputService;
        private CharacterModel _characterModel;
        
        public MainCharacterInput(CharacterModel characterModel)
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
        }
        
        private void OnHorizontalMovementUp(InputAction.CallbackContext inputAction)
        {
            _movement.x = inputAction.ReadValue<Single>();
        }
        
        private void OnVerticalMovementDown(InputAction.CallbackContext inputAction)
        {
            _movement.y = inputAction.ReadValue<Single>();
        }
        
        private void OnVerticalMovementUp(InputAction.CallbackContext inputAction)
        {
            _movement.y = inputAction.ReadValue<Single>();
        }
    }
}