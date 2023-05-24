using System;
using UnityEngine;
using Urd.Services;
using Urd.Utils;

namespace Urd.Character.Skill
{
    [Serializable] 
    public abstract class SkillController<TSkillModel> : ISkillController where TSkillModel : class, ISkillModel
    {
        protected CharacterModel _characterModel;
        protected ICharacterInput _characterInput;
        
        private IClockService _clockService;
        protected TSkillModel _skillModel;

        protected Vector2 _direction;
        private bool _isDoingSkill = false;

        protected float _skillTime;
        
        public virtual void Init(CharacterModel characterModel,
            ICharacterInput characterInput)
        {
            _characterModel = characterModel;
            SetInput(characterInput);
            
            _clockService = StaticServiceLocator.Get<IClockService>();
        }
        
        public virtual void Dispose()
        {
            _characterInput?.Dispose();
        }
        
        protected void SetModel(TSkillModel skillModel)
        {
            _skillModel = skillModel;
        }

        public void SetInput(ICharacterInput characterInput)
        {
            _characterInput = characterInput;
        }


        protected void OnSkillStatusChanged(bool isDoingSkill, Vector2 direction)
        {
            if(!CanDoSkill())
            {
                return;
            }
            
            if (!_isDoingSkill)
            {
                SetIsDoing(isDoingSkill, direction);
            }
        }

        private void SetIsDoing(bool isDoingSkill, Vector2 direction = default)
        {
            _isDoingSkill = isDoingSkill;
            
            if (isDoingSkill)
            {
                _direction = direction.normalized;
                BeginSkill(direction);
            }
        }
        
        protected virtual void BeginSkill(Vector2 direction)
        {
            _clockService.AddDelayCall(_skillModel.Duration, OnFinishSkill);
            _clockService.SubscribeToUpdate(SkillUpdate);

            _skillTime = 0;
        }

        protected virtual void SkillUpdate(float deltaTime)
        {
            _skillTime += deltaTime;
        }
        
        protected virtual void OnFinishSkill()
        {
            SetIsDoing(false);
            _clockService.UnSubscribeToUpdate(SkillUpdate);
            BeginCoolDown();
        }
        
        protected virtual void CoolDownUpdate(float deltaTime)
        {
            if (_skillModel.TimerModel.IsInCooldown)
            {
                _skillModel.TimerModel.DeductTime(deltaTime);
            }
        }
        
        private void BeginCoolDown()
        {
            if (!_skillModel.TimerModel.HasCooldown)
            {
                return;
            }
            _skillModel.TimerModel.BeginTimer(OnFinishCoolDown);
            _clockService.SubscribeToUpdate(CoolDownUpdate);
        }

        private void OnFinishCoolDown()
        {
            _clockService.UnSubscribeToUpdate(CoolDownUpdate);
        }

        protected virtual bool CanDoSkill()
        {
            return !_characterModel.SkillSetModel.IsSkill
                   && !_skillModel.TimerModel.IsInCooldown;
        }
    }
}