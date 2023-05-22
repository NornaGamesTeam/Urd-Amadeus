using System;
using UnityEngine;

namespace Urd.Character.Skill
{
    [Serializable] 
    public class MeleeAttackController : SkillController
    {
        private MeleeAttackModel _meleeAttackModel => _skillModel as MeleeAttackModel;
        
        public override void Init(CharacterModel characterModel, ICharacterInput characterInput)
        {
            base.Init(characterModel, characterInput);

            SetModel(_characterModel.SkillSetModel.MeleeAttackModel);
            
            _characterInput.OnAttackingChanged += OnSkillStatusChanged;
        }
        
        public override void Dispose()
        {
            _characterInput.OnAttackingChanged -= OnSkillStatusChanged;
            base.Dispose();
        }

        protected override void BeginSkill(Vector2 direction)
        {
            base.BeginSkill(direction);
            _characterModel.SkillSetModel.SetIsMeleeAttack(true);
        }

        protected override void OnFinishSkill()
        {
            base.OnFinishSkill();
            
            _characterModel.SkillSetModel.SetIsMeleeAttack(false);
        }
    }
}