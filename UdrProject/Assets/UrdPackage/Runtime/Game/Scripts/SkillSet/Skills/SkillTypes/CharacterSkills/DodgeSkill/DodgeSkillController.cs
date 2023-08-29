using System;

namespace Urd.Character.Skill
{
    [Serializable] 
    public class DodgeSkillController : SkillController<DodgeSkillModel>
    {
        public override void Init(ICharacterModel characterModel, ICharacterInput characterInput)
        {
            base.Init(characterModel, characterInput);

            SetModel(_characterModel.SkillSetModel.DodgeSkillModel);
            
            (_characterInput as MainCharacterInput).OnIsDodgingChanged += OnSkillStatusChanged;
        }

        public override void Dispose()
        {
            (_characterInput as MainCharacterInput).OnIsDodgingChanged -= OnSkillStatusChanged;
            base.Dispose();
        }
        
        protected override void SkillUpdate(float deltaTime)
        {
            base.SkillUpdate(deltaTime);
            
            var movement = _direction * _skillModel.Distance/_skillModel.Duration * deltaTime;
            _characterModel.MovementModel.TryModifyPhysicPosition(movement);
        }
    }
}