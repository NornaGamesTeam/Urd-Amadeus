using UnityEngine;

namespace Urd.Game.SkillTrees
{
    [System.Serializable]
    public abstract class BaseSkillConfig : ScriptableObject, ISkillConfig
    { 
        public abstract SkillType Type { get; protected set; }
    }
}
