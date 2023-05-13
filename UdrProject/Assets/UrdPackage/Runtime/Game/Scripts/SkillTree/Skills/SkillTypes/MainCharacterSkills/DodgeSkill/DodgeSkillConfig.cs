using UnityEngine;

namespace Urd.Game.SkillTrees
{
    [CreateAssetMenu(fileName = "NewDodgeSkill", menuName = "Urd/SkillTreeConfig/MainCharacterSkill/DodgeSkill", order = 1)]
    public class DashSkillConfig : BaseSkillConfig
    {
        [field: SerializeField]
        public float Distance { get; protected set; }
    }
}