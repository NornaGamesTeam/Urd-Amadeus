using MyBox;
using UnityEngine;
using UnityEngine.Serialization;
using Urd.Character.Skill;
using Urd.Models;
using Urd.UI;

namespace Urd.Character
{
    [System.Serializable]
    public class CharacterModel : ICharacterModel
    {
        public UICharacterConfig UICharacterConfig => CharacterConfig?.UICharacterConfig;
        
        [field: SerializeField, ReadOnly]
        public CharacterStatsModel CharacterStatsModel { get; private set; }
        
        [field: SerializeField, ReadOnly]
        public CharacterMovementModel MovementModel { get; private set; }
        
        [field: SerializeField, ReadOnly]
        public SkillSetModel SkillSetModel { get; private set; }

        [field: SerializeField, ReadOnly]
        public CharacterConfig CharacterConfig { get; private set; }

        public CharacterModel()
        {
            
        }
        public CharacterModel(CharacterConfig characterConfig)
        {
            CharacterConfig = characterConfig;
            MovementModel = new CharacterMovementModel(this);
            SkillSetModel = new SkillSetModel(this);
            CharacterStatsModel = new CharacterStatsModel(this);
        }
    }
}