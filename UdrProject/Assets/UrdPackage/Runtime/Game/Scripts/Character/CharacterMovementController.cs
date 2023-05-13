using System;
using Urd.Services;
using Urd.Utils;

namespace Urd.Character
{
    public class CharacterMovementController : IDisposable
    {
        private ICharacterInput _input;

        private CharacterModel _characterModel;
        
        public CharacterMovementController(CharacterModel characterModel)
        {
            _characterModel = characterModel;
            
            Init();
        }

        public void Init()
        {
            _input = new MainCharacterInput(_characterModel);
            
            SubscribeToUpdate();
        }
        
        public void Dispose()
        {
            _input?.Dispose();
        }

        public void SetInput(ICharacterInput newInput)
        {
            _input = newInput;
        }

        private void SubscribeToUpdate()
        {
            StaticServiceLocator.Get<IClockService>().SubscribeToUpdate(OnUpdate);
        }

        private void OnUpdate(float deltaTime)
        {
            if (_input.Movement.sqrMagnitude > 0)
            {
                _characterModel.CharacterMovement.SetRawNormalizedMovement(_input.Movement);
                _characterModel.CharacterMovement.ModifyPosition(_input.Movement * _characterModel.Speed * deltaTime);
                _characterModel.CharacterMovement.SetIsMoving(true);
            }
            else
            {
                _characterModel.CharacterMovement.SetIsMoving(false);
            }
        }
    }
}