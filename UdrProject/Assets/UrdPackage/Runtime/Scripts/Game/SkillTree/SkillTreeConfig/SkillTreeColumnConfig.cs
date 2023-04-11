using System.Collections.Generic;
using UnityEngine;

namespace Urd.Game.SkillTrees
{
    [System.Serializable]
    public class SkillTreeColumnConfig
    {
        [field: SerializeField]
        public List<SkillTreeColumnInfo> Levels { get; private set; }

        [System.Serializable]
        public class SkillTreeColumnInfo
        {
            [field: SerializeReference]
            public List<BaseSkillConfig> LevelSkills { get; private set; }
        }
    }
}