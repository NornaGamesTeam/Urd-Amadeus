using JetBrains.Annotations;
using MyBox;
using UnityEngine;
using Urd.Character.Skill;
using Urd.Services.Physics;

namespace Urd.Character
{
    public class CharacterInteractionBox : CharacterModelListener
    {
        [SerializeField]
        private Transform _interactionObject;
        
        private MainCharacterModel MainCharacterModel => _characterModel as MainCharacterModel;
        
        public override void Init()
        {
            // remove this from the start
            MainCharacterModel.CharacterMovement.OnAimDirectionChanged += OnAimDirectionChanged;
            MainCharacterModel.SkillSetModel.OnIsSkill += OnIsSkill;
        }
        
        private void OnIsSkill(bool onIsSkill)
        {
            
        }
        
        private void OnAimDirectionChanged(Vector2 aimDirection)
        {
            _interactionObject.transform.SetXY(
                transform.position.x + aimDirection.x * MainCharacterModel.InteractionObjectDistance,
                transform.position.y + aimDirection.y * MainCharacterModel.InteractionObjectDistance);
        }
    }
}
