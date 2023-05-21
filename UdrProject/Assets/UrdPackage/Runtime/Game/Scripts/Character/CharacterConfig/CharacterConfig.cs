using System.Collections.Generic;
using UnityEngine;
using Urd.Game.SkillTrees;

namespace Urd.Character
{
    public class CharacterConfig : ScriptableObject
    {
        [field: Header("Movement"), SerializeField] 
        public float Speed { get; private set; }

        [field: Header("Skills"),SerializeField] 
        public List<SkillConfig> DefaultSkillConfigs { get; private set; }

        [field: SerializeField] public SkillTreeConfig SkillTreeConfig { get; private set; }
    }
}
