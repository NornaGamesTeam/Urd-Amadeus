using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Urd.Models
{
    [System.Serializable]
    public class MovementModel
    {
        [field: SerializeField, MyBox.ReadOnly]
        public Vector2 Position { get; private set; }
        
        [field: SerializeField, MyBox.ReadOnly]
        public bool IsMoving { get; private set; }
        
        public event Action<Vector2> OnPositionChanged;
        public event Action<bool> OnIsMovingChanged;

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