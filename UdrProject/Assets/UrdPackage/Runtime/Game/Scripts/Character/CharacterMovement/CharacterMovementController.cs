using System;
using UnityEngine;
using Urd.Services;
using Urd.Utils;

namespace Urd.Character
{
    public class CharacterMovementController : IDisposable
    {
        private ICharacterInput _characterInput;
        private ICharacterModel _characterModel;

        private IClockService _clockService;
        
        public CharacterMovementController(ICharacterModel characterModel, ICharacterInput characterInput,
            Vector3 initialPosition)
        {
            _characterModel = characterModel;
            SetInput(characterInput);
            Init();
            
            _characterModel.CharacterMovement.SetPosition(initialPosition);
        }

        public void Init()
        {
            _clockService = StaticServiceLocator.Get<IClockService>();
        }

        public void SetInput(ICharacterInput newInput)
        {
            _characterInput = newInput;
            _characterInput.OnMovementChanged += OnMovementChanged;
            _characterInput.OnAimDirectionChanged += OnAimChanged;
        }
        
        public void Dispose()
        {
            _characterInput.OnMovementChanged -= OnMovementChanged;
            _characterInput.OnAimDirectionChanged -= OnAimChanged;
            _characterInput?.Dispose();
        }

        private void OnMovementChanged(Vector2 movement)
        {
            if (!CanMove())
            {
                return;
            }

            _characterModel.CharacterMovement.SetIsMoving(movement.sqrMagnitude > 0);
            if (_characterModel.CharacterMovement.IsMoving)
            {
                _characterModel.CharacterMovement.SetRawNormalizedMovement(movement);
                _characterModel.CharacterMovement.ModifyPosition(
                    movement * _characterModel.CharacterMovement.Speed * _clockService.DeltaTime);
            }
        }

        private bool CanMove()
        {
            return !_characterModel.SkillSetModel.IsSkill &&
                   !_characterModel.HitPointsModel.IsHit;
        }

        private void OnAimChanged(Vector2 aimDirection)
        {
            if (_characterModel.SkillSetModel.IsSkill)
            {
                return;
            }

            _characterModel.CharacterMovement.SetAimDirection(aimDirection);
        }
    }
}