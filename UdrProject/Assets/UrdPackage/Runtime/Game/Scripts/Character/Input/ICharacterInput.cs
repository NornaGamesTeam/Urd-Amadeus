using System;
using UnityEngine;
using Urd.Inputs;

namespace Urd.Character
{
    public interface ICharacterInput : IDisposable
    {
        void Init();
        event Action<Vector2> OnMovementChanged;
        event Action<Vector2> OnAimDirectionChanged;
        delegate void AttackDelegate(bool isAttacking, Vector2 attackDirection, SkillActionType skillActionType);
        event AttackDelegate OnAttackingChanged;
    }
}