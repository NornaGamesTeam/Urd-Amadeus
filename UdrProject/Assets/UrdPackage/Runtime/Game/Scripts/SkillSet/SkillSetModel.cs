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

        public bool IsSkill { get; private set; }
        public SkillModel DodgeSkill => DefaultSkills.Find(skill => skill.AnimParameter == CharacterAnimParameters.IS_DODGE);

        public event Action<SkillModel> OnSkillAction;
        public event Action<bool> OnIsSkill;


        public SkillSetModel(List<SkillConfig> defaultSkillConfigs, SkillTreeConfig skillTreeConfig)
        {
            DefaultSkills = new List<SkillModel>();
            for (int i = 0; i < defaultSkillConfigs.Count; i++)
            {
                DefaultSkills.Add(new SkillModel(defaultSkillConfigs[i]));
            }
            
            SkillTreeModel = new SkillTreeModel(skillTreeConfig);
        }

        public void SetIsDodging(bool inputIsDodging)
        {
            var skillModel = DefaultSkills.Find(skill => skill.AnimParameter == CharacterAnimParameters.IS_DODGE);
            if (skillModel == null)
            {
                return;
            }

            SetIsSkill(inputIsDodging);
            OnSkillAction?.Invoke(skillModel);
        }

        private void SetIsSkill(bool isSkill)
        {
            IsSkill = isSkill;
            OnIsSkill?.Invoke(IsSkill);
        }
    }
}
