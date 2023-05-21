using System;
using Urd.Services;
using Urd.Utils;

namespace Urd.Character.Skill
{
    [Serializable] 
    public abstract class SkillController : ISkillController
    {
        protected CharacterModel _characterModel;
        protected ICharacterInput _characterInput;
        
        private IClockService _clockService;
        protected ISkillModel _skillModel;

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
        
        protected void SetModel(ISkillModel skillModel)
        {
            _skillModel = skillModel;
        }

        public void SetInput(ICharacterInput characterInput)
        {
            _characterInput = characterInput;
        }
        
        protected void BeginSkill()
        {
            _clockService.AddDelayCall(_skillModel.Duration, OnFinishSkill);
            _clockService.SubscribeToUpdate(SkillUpdate);
        }

        protected virtual void SkillUpdate(float deltaTime) { }
        
        protected virtual void CoolDownUpdate(float deltaTime)
        {
            if (_skillModel.TimerModel.IsInCooldown)
            {
                _skillModel.TimerModel.DeductTime(deltaTime);
            }
        }

        protected virtual void OnFinishSkill()
        {
            _clockService.UnSubscribeToUpdate(SkillUpdate);
            BeginCoolDown();
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