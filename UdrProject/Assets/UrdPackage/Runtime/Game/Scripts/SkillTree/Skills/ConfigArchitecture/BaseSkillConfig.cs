using UnityEngine;

namespace Urd.Game.SkillTrees
{
    [System.Serializable]
    public abstract class BaseSkillConfig : ScriptableObject, ISkillConfig
    {
        [field: Header("Lock Properties"), SerializeField]
        public float LevelToUnlock { get; protected set; }
        
        [field: SerializeField]
        public virtual SkillType Type { get; protected set; }
        
        [field: SerializeField]
        public float Duration { get; protected set; }
        
    }
}
