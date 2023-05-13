using UnityEngine;
using Urd.Character.Skill;
using Urd.Game.SkillTrees;
using Urd.Models;

namespace Urd.Character
{
    [System.Serializable]
    public class CharacterModel
    {
        public float Speed => _characterConfig.Speed;
        [field: SerializeField]
        public CharacterMovementModel CharacterMovement { get; private set; } = new ();
        [field: SerializeField]
        public SkillSetModel SkillSetModel { get; private set; }
        
        private CharacterConfig _characterConfig;

        public CharacterModel(CharacterConfig characterConfig)
        {
            _characterConfig = characterConfig;
            SkillSetModel = new SkillSetModel(characterConfig.DefaultSkillConfigs, characterConfig.SkillTreeConfig);
        }
    }
}