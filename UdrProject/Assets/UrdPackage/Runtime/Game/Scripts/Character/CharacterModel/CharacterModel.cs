using MyBox;
using UnityEngine;
using Urd.Character.Skill;
using Urd.Models;
using Urd.UI;

namespace Urd.Character
{
    [System.Serializable]
    public class CharacterModel : ICharacterModel
    {
        public UICharacterConfig UICharacterConfig => _characterConfig?.UICharacterConfig;
        
        [field: SerializeField, ReadOnly]
        public CharacterHitPointsModel HitPointsModel { get; private set; }
        
        [field: SerializeField, ReadOnly]
        public CharacterMovementModel CharacterMovement { get; private set; }
        
        [field: SerializeField, ReadOnly]
        public SkillSetModel SkillSetModel { get; private set; }

        private CharacterConfig _characterConfig;

        public CharacterModel()
        {
            
        }

        public CharacterModel(CharacterConfig characterConfig)
        {
            _characterConfig = characterConfig;
            HitPointsModel = new CharacterHitPointsModel(_characterConfig);
            CharacterMovement = new CharacterMovementModel(_characterConfig);
            SkillSetModel = new SkillSetModel(characterConfig.DefaultSkillConfigs, characterConfig.SkillTreeConfig);
        }
    }
}