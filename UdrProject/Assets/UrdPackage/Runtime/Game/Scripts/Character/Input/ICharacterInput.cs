using System;
using UnityEngine;

namespace Urd.Character
{
    public interface ICharacterInput : IDisposable
    {
        event Action<Vector2> OnMovementChanged;
        event Action<Vector2> OnAimDirectionChanged;
        delegate void DodgeDelegate(bool isDodging, Vector2 dodgeDirection);
        event DodgeDelegate OnIsDodgingChanged;
        delegate void AttackDelegate(bool isDodging, Vector2 attackDirection);
        event AttackDelegate OnAttackingChanged;
    }
}