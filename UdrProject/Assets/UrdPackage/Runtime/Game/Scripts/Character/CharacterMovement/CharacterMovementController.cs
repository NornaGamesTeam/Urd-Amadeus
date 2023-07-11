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
                //ClampMovement(ref deltaMovement);
                _characterModel.CharacterMovement.ModifyPosition(deltaMovement);
                //_characterModel.CharacterMovement.ModifyPosition(deltaMovement);
                //_rigidbody2D.MovePosition(_rigidbody2D.position + deltaMovement);
            }
        }

        
        private void DoMovement(Vector2 deltaMovement)
        {
            var newDeltaMovement = _rigidbody2D.transform.localPosition;
            //*
            _rigidbody2D.transform.localPosition = Vector3.zero;
             _clockService.AddDelayCall(0.01f, () => DoMovement2(newDeltaMovement));
             /*/
            _characterModel.CharacterMovement.ModifyPosition(newDeltaMovement);
            Debug.Log($"Movement Before: {deltaMovement}" +
                      $"Movement After: {newDeltaMovement}");
            /**/
        }

        private void DoMovement2(Vector2 newDeltaMovement)
        {
            _characterModel.CharacterMovement.ModifyPosition(newDeltaMovement);
        }

        private void ClampMovement(ref Vector2 deltaMovement)
        {
            _rigidbody2D.MovePosition(_characterModel.CharacterMovement.Position + deltaMovement);
            var vector2 = deltaMovement;
            _clockService.AddDelayCall(0.01f, () => DoMovement(vector2));
            /*
            deltaMovement = _rigidbody2D.position - _characterModel.CharacterMovement.Position;
            _rigidbody2D.MovePosition(_characterModel.CharacterMovement.Position - deltaMovement);
            */
            
            /*
            var newDeltaMovement = deltaMovement;
            _rigidbody2D.MovePosition(_rigidbody2D.position + deltaMovement);

            List<ContactPoint2D> contacts = new List<ContactPoint2D>();
            _rigidbody2D.GetContacts(contacts);
            if (contacts.IsNullOrEmpty())
            {
                return;
            }
            
            Vector2 position = _rigidbody2D.position;

            float minX = position.x+newDeltaMovement.x;
            float maxX = position.x+newDeltaMovement.x;
            float minY = position.y+newDeltaMovement.y;
            float maxY = position.y+newDeltaMovement.y;
            //Vector2 position = _characterModel.CharacterMovement.Position;
            
            for (int i = 0; i < contacts.Count; i++)
            {
                if (minX > contacts[i].point.x)
                {
                    minX = contacts[i].point.x;
                }
                if (maxX < contacts[i].point.x)
                {
                    maxX = contacts[i].point.x;
                }
                if (minY > contacts[i].point.y)
                {
                    minY = contacts[i].point.y;
                }
                if (maxY < contacts[i].point.y)
                {
                    maxY = contacts[i].point.y;
                }
            }

            if (deltaMovement.x < 0)
            {
                newDeltaMovement.x = Mathf.Max(deltaMovement.x, position.x-maxX);
            }
            if (deltaMovement.x > 0)
            {
                newDeltaMovement.x = Mathf.Min(deltaMovement.x, position.x-minX);
            }
            
            if (deltaMovement.y < 0)
            {
                newDeltaMovement.y = Mathf.Max(deltaMovement.y, position.y-maxY);
            }
            if (deltaMovement.y > 0)
            {
                newDeltaMovement.y = Mathf.Min(deltaMovement.y, position.y-minY);
            }

            _rigidbody2D.MovePosition(_rigidbody2D.position - deltaMovement);

            Debug.Log($"minX: {minX} | maxX:{maxX} | minY: {minY} | maxY:{maxY}");
            Debug.Log($"deltaMovement: {deltaMovement} | newDeltaMovement:{newDeltaMovement}");
            deltaMovement = newDeltaMovement;
            /* */
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