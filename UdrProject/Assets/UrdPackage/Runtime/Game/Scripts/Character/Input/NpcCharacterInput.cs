using System;
using UnityEngine;

namespace Urd.Character
{
    public class NpcCharacterInput : ICharacterInput
    {
        public NpcCharacterInput(ICharacterModel characterModel)
        {
        }

        public void Dispose()
        {
        }

        public event Action<Vector2> OnMovementChanged;
        public event Action<Vector2> OnAimDirectionChanged;
        public event ICharacterInput.DodgeDelegate OnIsDodgingChanged;
        public event ICharacterInput.AttackDelegate OnAttackingChanged;
        public event ICharacterInput.InteractDelegate OnInteractChanged;
    }
}