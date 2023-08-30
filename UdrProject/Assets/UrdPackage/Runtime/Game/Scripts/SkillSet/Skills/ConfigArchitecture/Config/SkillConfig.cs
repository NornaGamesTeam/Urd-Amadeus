using UnityEngine;
using Urd.Character.Skill;
using Urd.UI;

namespace Urd.Game.SkillTrees
{
    [CreateAssetMenu(fileName = "SkillConfig", menuName = "Urd/SkillConfig/New Skill Config", order = 1)]
    public class SkillConfig : ScriptableObject, ISkillConfig
    {
        [field:SerializeReference, Header("Logic Class"), SubclassSelector]
        public ISkillController Controller { get; protected set; }
        [field: SerializeReference, SubclassSelector]
        public ISkillModel Model { get; protected set; }
    }
}
