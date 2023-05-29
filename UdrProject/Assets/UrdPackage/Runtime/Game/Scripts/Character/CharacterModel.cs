using MyBox;
using UnityEngine;
using Urd.Character.Skill;
using Urd.Game.SkillTrees;
using Urd.Models;

namespace Urd.Character
{
    [System.Serializable]
    public class CharacterModel
    {
        [field: SerializeField, ReadOnly]
        public CharacterHitPointsModel HitPoints { get; private set; }
        
        [field: SerializeField, ReadOnly]
        public CharacterMovementModel CharacterMovement { get; private set; }
        
        [field: SerializeField, ReadOnly]
        public SkillSetModel SkillSetModel { get; private set; }
        
        private CharacterConfig _characterConfig;

        public CharacterModel(CharacterConfig characterConfig)
        {
            _characterConfig = characterConfig;
            HitPoints = new CharacterHitPointsModel(_characterConfig);
            CharacterMovement = new CharacterMovementModel(_characterConfig);
            SkillSetModel = new SkillSetModel(characterConfig.DefaultSkillConfigs, characterConfig.SkillTreeConfig);
        }
    }
}