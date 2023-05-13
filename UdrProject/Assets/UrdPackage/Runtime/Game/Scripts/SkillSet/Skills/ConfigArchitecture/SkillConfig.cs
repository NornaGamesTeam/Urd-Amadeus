using UnityEngine;
using Urd.Character.Skill;

namespace Urd.Game.SkillTrees
{
    [System.Serializable]
    public abstract class SkillConfig : ScriptableObject, ISkillConfig
    {
        [field: SerializeField]
        public string Name { get; protected set; }
        
        [field: Header("Logic Class"), SerializeReference, SubclassSelector]
        public ISkillController Controller { get; protected set; }
        
        [field: Header("Lock Properties"), SerializeField]
        public int LevelToUnlock { get; protected set; }
        
        [field: SerializeField]
        public virtual SkillType Type { get; protected set; }
        
        [field: Header("Skill Properties"), SerializeField]
        public float Duration { get; protected set; }
        
    }
}
