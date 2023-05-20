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
        private IClockService _clockService;
        private DodgeSkillModel _dodgeSkill;
        private Vector2 _direction;
        
        public override void Init(CharacterModel characterModel, ICharacterInput characterInput)
        {
            base.Init(characterModel, characterInput);

            _dodgeSkill = _characterModel.SkillSetModel.DodgeSkillModel;
            
            characterInput.OnIsDodgingChanged += OnIsDodgingChanged;
            _clockService = StaticServiceLocator.Get<IClockService>();
        }


        public override void Dispose()
        {
            base.Dispose();
            _characterInput.OnIsDodgingChanged -= OnIsDodgingChanged;
        }

        private void OnIsDodgingChanged(bool isDodging, Vector2 dodgeDirection)
        {
            if (_characterModel.SkillSetModel.IsSkill)
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
            _clockService.AddDelayCall(_dodgeSkill.Duration, OnFinishDodge);
            _clockService.SubscribeToUpdate(DodgeUpdate);
            _direction = dodgeDirection.normalized;
        }

        private void DodgeUpdate(float deltaTime)
        {
            var movement = _direction * _dodgeSkill.Distance/_dodgeSkill.Duration * deltaTime;
            _characterModel.CharacterMovement.ModifyPosition(movement);
        }

        private void OnFinishDodge()
        {
            _clockService.UnSubscribeToUpdate(DodgeUpdate);
            SetIsDodging(false);
        }
    }
}