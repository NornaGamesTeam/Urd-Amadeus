using System;
using System.Collections.Generic;
using MyBox;
using UnityEngine;
using Urd.Character.Skill;
using Urd.Game.SkillTrees;
using Urd.UI;

namespace Urd.Character
{
    [CreateAssetMenu(fileName = "NewCharacterConfig", menuName = "Urd/Character/New CharacterConfig", order = 1)]
    public class CharacterConfig : ScriptableObject
    {
        [field: Header("Basic Stats")]
        [field: SerializeField] 
        public float HitPoints { get; private set; }
        [field: SerializeField] 
        public float Attack { get; private set; }
        [field: SerializeField] 
        public float Defense { get; private set; }
        [field: SerializeField] 
        public float SpecialAttack { get; private set; }
        [field: SerializeField] 
        public float SpecialDefense { get; private set; }
        [field: SerializeField, Range(0f,1f)] 
        public float CriticChance { get; private set; }
        [field: SerializeField] 
        public List<ElementModsInfo> Vulnerabilities { get; private set; }
        [field: SerializeField] 
        public List<ElementModsInfo> Resistances { get; private set; }
        
        [field: SerializeReference] 
        public SkillConfig HitSkillConfig { get; private set; }
        
        [field: Header("Movement"), SerializeField] 
        public float Speed { get; private set; }
        
        [field: Header("Skills"),SerializeField] 
        public List<SkillConfig> DefaultSkillConfigs { get; private set; }

        [field: SerializeField] public SkillTreeConfig SkillTreeConfig { get; private set; }
        [field: SerializeField, Header("UI"), DisplayInspector] public UICharacterConfig UICharacterConfig { get; private set; }
        
        [field: SerializeField, Header("Physics"), DisplayInspector] public Rigidbody2D CharacterPhysics { get; private set; }
    }
}
