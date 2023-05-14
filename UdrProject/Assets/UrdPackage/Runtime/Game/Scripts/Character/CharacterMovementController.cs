using System;
using UnityEngine;
using Urd.Services;
using Urd.Utils;

namespace Urd.Character
{
    public class CharacterMovementController : IDisposable
    {
        private ICharacterInput _characterInput;
        private CharacterModel _characterModel;

        private IClockService _clockService;
        
        public CharacterMovementController(CharacterModel characterModel, ICharacterInput characterInput)
        {
            _characterModel = characterModel;
            SetInput(characterInput);
            Init();
        }

        public void Init()
        {
            _clockService = StaticServiceLocator.Get<IClockService>();

            SubscribeToUpdate();
        }

        public void SetInput(ICharacterInput newInput)
        {
            _characterInput = newInput;

            _characterInput.OnMovementChanged += OnMovementChanged;
        }
        
        public void Dispose()
        {
            _characterInput.OnMovementChanged -= OnMovementChanged;
            
            _characterInput?.Dispose();
        }

        private void OnMovementChanged(Vector2 obj)
        {
            if (_characterModel.SkillSetModel.IsSkill)
            {
                return;
            }
            
            _characterModel.CharacterMovement.SetIsMoving(_characterInput.Movement.sqrMagnitude > 0);
            if (!_characterModel.SkillSetModel.IsSkill && _characterModel.CharacterMovement.IsMoving)
            {
                _characterModel.CharacterMovement.SetRawNormalizedMovement(_characterInput.Movement);
                _characterModel.CharacterMovement.ModifyPosition(_characterInput.Movement * _characterModel.Speed * _clockService.DeltaTime);
            }
        }


        private void SubscribeToUpdate()
        {
            //StaticServiceLocator.Get<IClockService>().SubscribeToUpdate(OnUpdate);
        }
    }
}