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
        private IPhysicsService _physicsService;
        private Rigidbody2D _rigidbody2D;
        
        public CharacterMovementController(ICharacterModel characterModel, ICharacterInput characterInput,
            Vector3 initialPosition, Rigidbody2D rigidbody2D)
        {
            _characterModel = characterModel;
            SetInput(characterInput);
            Init();
            _rigidbody2D = rigidbody2D;
            
            _characterModel.CharacterMovement.SetPosition(initialPosition);
            _rigidbody2D.position = _characterModel.CharacterMovement.Position;
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

                var deltaMovement = movement * _characterModel.CharacterMovement.Speed * _clockService.DeltaTime;
                Move(deltaMovement);
                /*
                ClampMovement(ref deltaMovement);
                Debug.Log($"Movement Before: {movement * _characterModel.CharacterMovement.Speed * _clockService.DeltaTime}" +
                          $"Movement After: {deltaMovement}");
                _characterModel.CharacterMovement.ModifyPosition(deltaMovement);
                */
            }
        }

        private void Move(Vector2 deltaMovement)
        {
            _rigidbody2D.MovePosition(_characterModel.CharacterMovement.Position + deltaMovement);
            //_rigidbody2D.GetContacts()
            _clockService.AddDelayCall(0.01f,DoMovement);
        }

        private void DoMovement()
        {
            var deltaMovement = _rigidbody2D.position - _characterModel.CharacterMovement.Position;
            _characterModel.CharacterMovement.ModifyPosition(deltaMovement);
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