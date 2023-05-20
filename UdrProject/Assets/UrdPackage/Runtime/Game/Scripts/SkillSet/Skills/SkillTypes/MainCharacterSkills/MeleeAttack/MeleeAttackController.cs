using System;
using UnityEngine;
using Urd.Services;
using Urd.Utils;

namespace Urd.Character.Skill
{
    [Serializable] 
    public class MeleeAttackController : SkillController
    {
        private bool _isAttacking;
        private IClockService _clockService;
        private MeleeAttackModel _meleeAttackModel;
        private Vector2 _direction;
        
        public override void Init(CharacterModel characterModel, ICharacterInput characterInput)
        {
            base.Init(characterModel, characterInput);

            _meleeAttackModel = _characterModel.SkillSetModel.MeleeAttackModel;
            
            _characterInput.OnAttackingChanged += OnIsAttackingChanged;
            _clockService = StaticServiceLocator.Get<IClockService>();
        }

        public override void Dispose()
        {
            _characterInput.OnAttackingChanged -= OnIsAttackingChanged;
            base.Dispose();
        }

        private void OnIsAttackingChanged(bool isAttacking, Vector2 attackDirection)
        {
            if (_characterModel.SkillSetModel.IsSkill)
            {
                return;
            }
            
            if (!_isAttacking)
            {
                SetIsAttacking(isAttacking, attackDirection);
            }
        }

        private void SetIsAttacking(bool isAttacking, Vector2 attackDirection = default)
        {
            _isAttacking = isAttacking;
            _characterModel.SkillSetModel.SetIsMeleeAttack(_isAttacking);

            if (isAttacking)
            {
                BeginMeleeAttack(attackDirection);
            }
        }

        private void BeginMeleeAttack(Vector2 attackDirection)
        {
            _clockService.AddDelayCall(_meleeAttackModel.Duration, OnFinishAttack);
            _clockService.SubscribeToUpdate(AttackUpdate);
            _direction = attackDirection.normalized;
        }

        private void AttackUpdate(float deltaTime)
        {
        }

        private void OnFinishAttack()
        {
            _clockService.UnSubscribeToUpdate(AttackUpdate);
            SetIsAttacking(false);
        }
    }
}