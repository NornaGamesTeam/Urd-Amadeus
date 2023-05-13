using System.Collections.Generic;
using UnityEngine;

namespace Urd.Game.SkillTrees
{
    [CreateAssetMenu(fileName = "NewSkillTreeConfig", menuName = "Urd/SkillTreeConfig/NewSkillTreeConfig", order = 1)]
    public class SkillTreeConfig : ScriptableObject
    {
        [field: SerializeField]
        public int PointPerLevel { get; private set; }
        [field: SerializeField]
        public List<SkillTreeColumnConfig> Columns { get; private set; }
    }
}