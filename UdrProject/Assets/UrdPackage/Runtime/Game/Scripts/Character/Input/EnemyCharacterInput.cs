using System;
using UnityEngine;

namespace Urd.Character
{
    public class EnemyCharacterInput : CharacterInput
    {
        public EnemyCharacterInput(ICharacterModel characterModel) : base(characterModel) { }
        
        public void SetMovementVector(Vector2 movement)
        {
            _movement = movement;
        }
        
        public void SetAimDirectionVector(Vector2 aimDirection)
        {
            _aimDirection = aimDirection;
        }
    }
}