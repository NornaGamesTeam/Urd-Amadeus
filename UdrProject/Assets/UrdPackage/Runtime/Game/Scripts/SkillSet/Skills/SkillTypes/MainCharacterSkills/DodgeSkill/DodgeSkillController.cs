using System;
using UnityEngine;
using Urd.Services;
using Urd.Utils;

namespace Urd.Character.Skill
{
    [Serializable] 
    public class DodgeSkillController : SkillController
    {
        private bool _isDodging;
        private Vector2 _direction;
        
        private DodgeSkillModel _dodgeSkillModel => _skillModel as DodgeSkillModel;

        public override void Init(CharacterModel characterModel, ICharacterInput characterInput)
        {
            base.Init(characterModel, characterInput);

            SetModel(_characterModel.SkillSetModel.DodgeSkillModel);
            
            characterInput.OnIsDodgingChanged += OnIsDodgingChanged;
        }

        public override void Dispose()
        {
            _characterInput.OnIsDodgingChanged -= OnIsDodgingChanged;
            base.Dispose();
        }

        private void OnIsDodgingChanged(bool isDodging, Vector2 dodgeDirection)
        {
            if(!CanDoSkill())
            {
                return;
            }
            
            if (!_isDodging)
            {
                SetIsDodging(isDodging, dodgeDirection);
            }
        }
        
        private void SetIsDodging(bool isDodging, Vector2 dodgeDirection = default)
        {
            _isDodging = isDodging;
            _characterModel.SkillSetModel.SetIsDodging(_isDodging);

            if (isDodging)
            {
                BeginDodge(dodgeDirection);
            }
        }

        private void BeginDodge(Vector2 dodgeDirection)
        {
            BeginSkill();
            _direction = dodgeDirection.normalized;
        }
        
        protected override void SkillUpdate(float deltaTime)
        {
            base.SkillUpdate(deltaTime);
            
            var movement = _direction * _dodgeSkillModel.Distance/_dodgeSkillModel.Duration * deltaTime;
            _characterModel.CharacterMovement.ModifyPosition(movement);
        }

        protected override void OnFinishSkill()
        {
            base.OnFinishSkill();
            
            SetIsDodging(false);
        }
    }
}