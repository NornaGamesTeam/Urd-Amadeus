using System;
using UnityEngine;

namespace Urd.Character.Skill
{
    [Serializable] 
    public class DodgeSkillController : SkillController<DodgeSkillModel>
    {
        public override void Init(CharacterModel characterModel, ICharacterInput characterInput)
        {
            base.Init(characterModel, characterInput);

            SetModel(_characterModel.SkillSetModel.DodgeSkillModel);
            
            characterInput.OnIsDodgingChanged += OnSkillStatusChanged;
        }

        public override void Dispose()
        {
            _characterInput.OnIsDodgingChanged -= OnSkillStatusChanged;
            base.Dispose();
        }
        
        protected override void BeginSkill(Vector2 direction)
        {
            base.BeginSkill(direction);            
            _characterModel.SkillSetModel.SetIsDodging(true);
        }
        
        protected override void SkillUpdate(float deltaTime)
        {
            base.SkillUpdate(deltaTime);
            
            var movement = _direction * _skillModel.Distance/_skillModel.Duration * deltaTime;
            _characterModel.CharacterMovement.ModifyPosition(movement);
        }

        protected override void OnFinishSkill()
        {
            base.OnFinishSkill();
            
            _characterModel.SkillSetModel.SetIsDodging(false);
        }
    }
}