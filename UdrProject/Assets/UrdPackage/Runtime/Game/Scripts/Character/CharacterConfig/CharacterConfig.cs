using System.Collections.Generic;
using UnityEngine;
using Urd.Game.SkillTrees;
using Urd.UI;

namespace Urd.Character
{
    public class CharacterConfig : ScriptableObject
    {
        [field: Header("Hit Points"), SerializeField] 
        public float HitPoints { get; private set; }
        
        [field: SerializeReference] 
        public SkillConfig HitSkillConfig { get; private set; }
        
        [field: Header("Movement"), SerializeField] 
        public float Speed { get; private set; }
        
        [field: Header("Skills"),SerializeField] 
        public List<SkillConfig> DefaultSkillConfigs { get; private set; }

        [field: SerializeField] public SkillTreeConfig SkillTreeConfig { get; private set; }
        [field: SerializeField, Header("UI")] public UICharacterConfig UICharacterConfig { get; private set; }
    }
}
