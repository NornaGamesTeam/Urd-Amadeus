using UnityEngine;

namespace Urd.Game.SkillTrees
{
    [CreateAssetMenu(fileName = "NewDodgeSkill", menuName = "Urd/SkillTreeConfig/MainCharacterSkill/DodgeSkill", order = 1)]
    public class DodgeSkillConfig : SkillConfig
    {
        [field: SerializeField, Header("Specific properties")]
        public float Distance { get; protected set; }
    }
}