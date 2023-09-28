using System;
using UnityEngine;
using UnityEngine.AI;
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

            InitRightBody(rigidbody2D);
            rigidbody2D.GetComponent<NavMeshAgent>().updateUpAxis = false;
            _characterModel.MovementModel.ForceSetPhysicPosition(initialPosition);
            _characterModel.MovementModel.SetPosition(initialPosition);
            _rigidbody2D.position = _characterModel.MovementModel.Position;
        }

        private void InitRightBody(Rigidbody2D rigidbody2D)
        {
            //_rigidbody2D = GameObject.Instantiate(_characterModel.CharacterMovement.CharacterPhysics);
            _rigidbody2D = rigidbody2D;
            _rigidbody2D.inertia = 0; 
            _rigidbody2D.drag = 0;
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
            _characterModel.MovementModel.OnPhysicPositionChanged += OnPhysicPositionChanged;
            _clockService.SubscribeToFixedUpdate(OnSetPhysicPosition);
        }

        public void Dispose()
        {
            _characterInput.OnMovementChanged -= OnMovementChanged;
            _characterInput.OnAimDirectionChanged -= OnAimChanged;
            _characterInput?.Dispose();
            _characterModel.MovementModel.OnPhysicPositionChanged -= OnPhysicPositionChanged;
            _clockService.UnSubscribeToFixedUpdate(OnSetPhysicPosition);
        }

        private void OnMovementChanged(Vector2 movement)
        {
            if (!CanMove())
            {
                return;
            }

            _characterModel.MovementModel.SetIsMoving(movement.sqrMagnitude > 0);
            if (_characterModel.MovementModel.IsMoving)
            {
                _characterModel.MovementModel.SetRawNormalizedMovement(movement);

                var deltaMovement = _characterModel.MovementModel.Position + movement * _characterModel.MovementModel.Speed * _clockService.DeltaTime;
                _characterModel.MovementModel.TrySetPhysicPosition(deltaMovement);
            }
        }

        private void OnPhysicPositionChanged(Vector2 characterMovementPhysicPosition)
        {
            _rigidbody2D.MovePosition(characterMovementPhysicPosition);
        }

        private void OnSetPhysicPosition(float fixedDeltaTime)
        {
            _characterModel.MovementModel.ForceSetPhysicPosition(_rigidbody2D.transform.position);
            _characterModel.MovementModel.SetPosition(_rigidbody2D.transform.position);
            if (_characterModel.MovementModel.Position != _rigidbody2D.position)
            {
                Debug.Log($"POS: {_characterModel.MovementModel.Position}  PH:{_rigidbody2D.position}");
            }
        }

        private bool CanMove()
        {
            return !_characterModel.SkillSetModel.IsDoingSkill &&
                   !_characterModel.CharacterStatsModel.IsHit;
        }

        private void OnAimChanged(Vector2 aimDirection)
        {
            if (_characterModel.SkillSetModel.IsDoingSkill)
            {
                return;
            }

            _characterModel.MovementModel.SetAimDirection(aimDirection);
        }
    }
}