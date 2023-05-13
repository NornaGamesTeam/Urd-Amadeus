using System;
using System.Collections.Generic;
using UnityEngine;
using Urd.Game.SkillTrees;

namespace Urd.Character.Skill
{
    [System.Serializable]
    public class SkillSetModel
    {
        [field: SerializeField]
        public List<SkillModel> DefaultSkills { get; private set; }
        [field: SerializeField]
        public SkillTreeModel SkillTreeModel { get; private set; }

        public event Action<SkillModel> OnSkillAction;
        
        public SkillSetModel(List<SkillConfig> defaultSkillConfigs, SkillTreeConfig skillTreeConfig)
        {
            DefaultSkills = new List<SkillModel>();
            for (int i = 0; i < defaultSkillConfigs.Count; i++)
            {
                DefaultSkills.Add(new SkillModel(defaultSkillConfigs[i]));
            }
            
            SkillTreeModel = new SkillTreeModel(skillTreeConfig);
        }
    }
}
