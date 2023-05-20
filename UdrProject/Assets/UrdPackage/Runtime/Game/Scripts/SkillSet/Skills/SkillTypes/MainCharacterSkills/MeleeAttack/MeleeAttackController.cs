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
        private MeleeAttackModel _meleeAttackModel => _skillModel as MeleeAttackModel;
        private Vector2 _direction;
        
        public override void Init(CharacterModel characterModel, ICharacterInput characterInput)
        {
            base.Init(characterModel, characterInput);

            SetModel(_characterModel.SkillSetModel.MeleeAttackModel);
            
            _characterInput.OnAttackingChanged += OnIsAttackingChanged;
        }
        
        public override void Dispose()
        {
            _characterInput.OnAttackingChanged -= OnIsAttackingChanged;
            base.Dispose();
        }

        private void OnIsAttackingChanged(bool isAttacking, Vector2 attackDirection)
        {
            if(!CanDoSkill())
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
            BeginSkill();
            _direction = attackDirection.normalized;
        }

        protected override void OnFinishSkill()
        {
            SetIsAttacking(false);
        }
    }
}