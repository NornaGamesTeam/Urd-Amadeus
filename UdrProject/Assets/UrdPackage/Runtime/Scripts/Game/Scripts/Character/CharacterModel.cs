using System;
using UnityEngine;

namespace Urd.Character
{
    [System.Serializable]
    public class CharacterModel
    {
        public float Speed => _characterConfig.Speed;
        
        [field: SerializeField, MyBox.ReadOnly]
        public Vector2 Position { get; private set; }
        
        private CharacterConfig _characterConfig;

        public event Action<Vector2> OnPositionChanged;
        
        public CharacterModel(CharacterConfig characterConfig)
        {
            _characterConfig = characterConfig;
        }

        public void ModifyPosition(Vector2 movement)
        {
            SetPosition(Position + movement);
        }
        
        public void SetPosition(Vector2 newPosition)
        {
            if (Position == newPosition)
            {
                return;
            }
            
            Position = newPosition;
            OnPositionChanged?.Invoke(Position);
        }
    }
}