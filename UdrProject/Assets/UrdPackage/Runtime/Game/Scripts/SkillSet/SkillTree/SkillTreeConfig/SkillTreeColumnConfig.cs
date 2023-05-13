using System.Collections.Generic;
using MyBox;
using UnityEngine;

namespace Urd.Game.SkillTrees
{
    [System.Serializable]
    public class SkillTreeColumnConfig
    {
        [field: SerializeField]
        public string ColumnName { get; private set; } 
        [field: SerializeField]
        public List<SkillTreeColumnInfo> Levels { get; private set; }

        [System.Serializable]
        public class SkillTreeColumnInfo
        {
            [field: SerializeField, DisplayInspector]
            public List<SkillConfig> LevelSkills { get; private set; }
        }
    }
}