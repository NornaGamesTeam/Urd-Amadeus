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
        public List<ISkillModel> DefaultSkills { get; private set; }
        [field: SerializeField]
        public SkillTreeModel SkillTreeModel { get; private set; }

        public bool IsSkill { get; private set; }
        public DodgeSkillModel DodgeSkill => DefaultSkills.Find(skill => skill is DodgeSkillModel) as DodgeSkillModel;

        public event Action<ISkillModel> OnSkillAction;
        public event Action<bool> OnIsSkill;


        public SkillSetModel(List<SkillConfig> defaultSkillConfigs, SkillTreeConfig skillTreeConfig)
        {
            DefaultSkills = new List<ISkillModel>();
            for (int i = 0; i < defaultSkillConfigs.Count; i++)
            {
                var model = defaultSkillConfigs[i].Model;
                model.SetConfig(defaultSkillConfigs[i]);
                DefaultSkills.Add(model);
            }
            
            SkillTreeModel = new SkillTreeModel(skillTreeConfig);
        }

        public void SetIsDodging(bool isDodging)
        {
            var dodgeSkill = DodgeSkill;
            if (dodgeSkill == null)
            {
                return;
            }

            dodgeSkill.SetIsActive(isDodging);
            SetIsSkill(isDodging);
            if (isDodging)
            {
                OnSkillAction?.Invoke(dodgeSkill);
            }
        }

        private void SetIsSkill(bool isSkill)
        {
            IsSkill = isSkill;
            OnIsSkill?.Invoke(IsSkill);
        }
    }
}
