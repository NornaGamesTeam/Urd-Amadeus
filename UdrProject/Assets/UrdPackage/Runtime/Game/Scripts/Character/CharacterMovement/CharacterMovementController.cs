using System;
using UnityEngine;
using UnityEngine.Networking;
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
            Init();
            SetInput(characterInput);
            _rigidbody2D = rigidbody2D;
            _rigidbody2D.inertia = 0; 
            _rigidbody2D.drag = 0;
            
            _characterModel.CharacterMovement.ForceSetPhysicPosition(initialPosition);
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
            _characterModel.CharacterMovement.OnPhysicPositionChanged += OnPhysicPositionChanged;
            _clockService.SubscribeToFixedUpdate(OnSetPhysicPosition);
        }

        public void Dispose()
        {
            _characterInput.OnMovementChanged -= OnMovementChanged;
            _characterInput.OnAimDirectionChanged -= OnAimChanged;
            _characterInput?.Dispose();
            _characterModel.CharacterMovement.OnPhysicPositionChanged -= OnPhysicPositionChanged;
            _clockService.UnSubscribeToFixedUpdate(OnSetPhysicPosition);
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

                var deltaMovement = _characterModel.CharacterMovement.Position + movement * _characterModel.CharacterMovement.Speed * _clockService.DeltaTime;
                _characterModel.CharacterMovement.TrySetPhysicPosition(deltaMovement);
            }
        }

        private void OnPhysicPositionChanged(Vector2 characterMovementPhysicPosition)
        {
            _rigidbody2D.MovePosition(characterMovementPhysicPosition);
            //_clockService.AddDelayCall(0.01f, OnSetPhysicPosition);
        }

        private void OnSetPhysicPosition(float fixedDeltaTime)
        {
            _characterModel.CharacterMovement.ForceSetPhysicPosition(_rigidbody2D.transform.position);
            _characterModel.CharacterMovement.SetPosition(_rigidbody2D.transform.position);
            Debug.Log($"POS: {_characterModel.CharacterMovement.Position}  PH:{_rigidbody2D.position}");
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