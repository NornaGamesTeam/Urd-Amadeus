using UnityEngine;
using Urd.Character.Skill;
using Urd.UI;

namespace Urd.Game.SkillTrees
{
    [System.Serializable]
    public abstract class SkillConfig : ScriptableObject, ISkillConfig
    {
        [field: SerializeField]
        public string Name { get; protected set; }
        
        [field:  Header("Logic Class"), SerializeReference, SubclassSelector]
        public ISkillModel Model { get; protected set; }
        [field:SerializeReference, SubclassSelector]
        public ISkillController Controller { get; protected set; }
        
        [field: Header("Lock Properties"), SerializeField]
        public int LevelToUnlock { get; protected set; }
        
        [field: SerializeField]
        public virtual SkillType Type { get; protected set; }
        
        [field: Header("Skill Graphic"), SerializeReference, SubclassSelector]
        public ISkillAnimationModel SkillAnimationModel { get; protected set; }

        [field: SerializeField]
        public UISkillConfig UISkillConfig { get; protected set; }

        [field: Header("Skill Properties"), Header("Generic Properties"), SerializeField]
        public float CoolDown { get; protected set; }
        [field: SerializeField]
        public float Duration { get; protected set; }
    }
}
