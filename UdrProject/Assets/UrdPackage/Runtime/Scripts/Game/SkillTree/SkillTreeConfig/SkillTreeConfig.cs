using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Urd.Game.SkillTrees
{
    public class SkillTreeConfig : ScriptableObject
    {
        [field: SerializeField]
        public List<SkillTreeColumnConfig> Columns { get; private set; }
    }
}