using UnityEngine;
using Urd.Character.Skill;
using Urd.UI;

namespace Urd.Game.SkillTrees
{
    public class SkillConfig : ScriptableObject, ISkillConfig
    {
        [field:SerializeReference, Header("Logic Class"), SubclassSelector]
        public ISkillController Controller { get; protected set; }
        [field: SerializeReference, SubclassSelector]
        public ISkillModel Model { get; protected set; }
    }
}
