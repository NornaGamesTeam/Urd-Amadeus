using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using Urd.Game.SkillTrees;

namespace Urd.Character.Skill
{
    [System.Serializable]
    public class SkillSetModel
    {
        [field: SerializeReference]
        public List<ISkillModel> DefaultSkills { get; private set; }
        [field: SerializeField]
        public SkillTreeModel SkillTreeModel { get; private set; }

        public bool IsSkill { get; private set; }
        public MeleeAttackModel MeleeAttackModel => DefaultSkills.Find(skill => skill is MeleeAttackModel) as MeleeAttackModel;
        public DodgeSkillModel DodgeSkillModel => DefaultSkills.Find(skill => skill is DodgeSkillModel) as DodgeSkillModel;

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

        public TModel GetSkillModel<TModel>() where TModel : class
        {
            return DefaultSkills.Find(skill => skill is TModel) as TModel;
        }

        public void SetIsSkill<TSkillModel>(bool isSkill) where TSkillModel : class, ISkillModel
        {
            var skillModel = GetSkillModel<TSkillModel>();
            if (skillModel == null)
            {
                return;
            }
            
            skillModel.SetIsActive(isSkill);
            SetIsSkill(isSkill);
            if (isSkill)
            {
                OnSkillAction?.Invoke(skillModel);
            }
        }

        private void SetIsSkill(bool isSkill)
        {
            IsSkill = isSkill;
            OnIsSkill?.Invoke(IsSkill);
        }
    }
}
