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
        public CharacterStatsModel CharacterStatsModel { get; private set; }
        
        [field: SerializeField, ReadOnly]
        public CharacterMovementModel MovementModel { get; private set; }
        
        [field: SerializeField, ReadOnly]
        public SkillSetModel SkillSetModel { get; private set; }

        protected CharacterConfig _characterConfig;

        public CharacterModel()
        {
            
        }
        public CharacterModel(CharacterConfig characterConfig)
        {
            _characterConfig = characterConfig;
            MovementModel = new CharacterMovementModel(_characterConfig);
            SkillSetModel = new SkillSetModel(characterConfig.DefaultSkillConfigs, characterConfig.SkillTreeConfig);
        }
    }
}