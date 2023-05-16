using System;
using UnityEngine;

namespace Urd.Character
{
    public interface ICharacterInput : IDisposable
    {
        public Vector2 Movement { get; }
        public Vector2 AimDirection { get; }
        public bool IsDodging { get; }

        event Action<Vector2> OnMovementChanged;
        event Action<Vector2> OnAimDirectionChanged;
        event Action<bool> OnIsDodgingChanged;
    }
}