using System;
using UnityEngine;

namespace Urd.Models
{
    [System.Serializable]
    public class CharacterMovementModel
    {
        [field: SerializeField, MyBox.ReadOnly]
        public Vector2 RawNormalizedMovement { get; private set; }
        
        [field: SerializeField, MyBox.ReadOnly]
        public Vector2 Position { get; private set; }
        
        [field: SerializeField, MyBox.ReadOnly]
        public bool IsMoving { get; private set; }
        
        public event Action<Vector2> OnRawNormalizedMovementChanged;
        public event Action<Vector2> OnPositionChanged;
        public event Action<bool> OnIsMovingChanged;

        public void SetRawNormalizedMovement(Vector2 rawNormalizedMovement)
        {
            if (rawNormalizedMovement == RawNormalizedMovement)
            {
                return;
            }

            RawNormalizedMovement = rawNormalizedMovement;
            OnRawNormalizedMovementChanged?.Invoke(rawNormalizedMovement);
        }
        
        public void ModifyPosition(Vector2 movement) => SetPosition(Position + movement);
        public void SetPosition(Vector2 newPosition)
        {
            if (Position == newPosition)
            {
                return;
            }
            
            Position = newPosition;
            OnPositionChanged?.Invoke(Position);
        }

        public void SetIsMoving(bool isMoving)
        {
            if (IsMoving == isMoving)
            {
                return;
            }
            
            IsMoving = isMoving;
            OnIsMovingChanged?.Invoke(IsMoving);
        }
    }
}