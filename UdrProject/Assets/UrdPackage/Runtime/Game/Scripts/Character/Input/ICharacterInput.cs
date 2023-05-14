using System;
using UnityEngine;

namespace Urd.Character
{
    public interface ICharacterInput : IDisposable
    {
        public Vector2 Movement { get; }
        public bool IsDodging { get; }

        event Action<Vector2> OnMovementChanged;
        event Action<bool> OnIsDodgingChanged;
    }
}