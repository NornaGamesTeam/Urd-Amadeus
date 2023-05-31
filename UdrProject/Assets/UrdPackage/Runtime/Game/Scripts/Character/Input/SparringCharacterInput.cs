using System;
using UnityEngine;

namespace Urd.Character
{
    public class SparringCharacterInput : ICharacterInput
    {
        public SparringCharacterInput(CharacterModel characterModel)
        {
        }

        public void Dispose()
        {
        }

        public event Action<Vector2> OnMovementChanged;
        public event Action<Vector2> OnAimDirectionChanged;
        public event ICharacterInput.DodgeDelegate OnIsDodgingChanged;
        public event ICharacterInput.AttackDelegate OnAttackingChanged;
    }
}