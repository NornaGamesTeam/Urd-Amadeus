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

        public List<(SkillSlotType, ISkillModel)> EquippedSkills;
        public List<(int, ISkillModel)> Skills;
        
        public event Action<ISkillModel> OnSkillAction;
        public event Action<bool> OnIsSkill;

        public SkillSetModel(CharacterModel characterModel)
        {
            var defaultSkillConfigs = characterModel.CharacterConfig.DefaultSkillConfigs;

            DefaultSkills = new List<ISkillModel>();
            for (int i = 0; i < defaultSkillConfigs.Count; i++)
            {
                var model = defaultSkillConfigs[i].Model;
                model.SetConfig(defaultSkillConfigs[i]);
                DefaultSkills.Add(model);
            }
            
            SkillTreeModel = new SkillTreeModel(characterModel.CharacterConfig.SkillTreeConfig);
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
        
        public float GetPassiveModificationFor(StatType statType)
        {
            var passiveSkills = Skills.FindAll(skill => skill.Item2.Type == SkillType.Pasive);

            float factor = 1;
            for (int i = 0; i < passiveSkills.Count; i++)
            {
                var passiveSkillOfStatType = passiveSkills[i].Item2 as PassiveSkillModel;
                if (passiveSkillOfStatType.AffectedElement == statType)
                {
                    factor += passiveSkillOfStatType.Factor;
                }
            }
            
            return factor;
        }
    }
}
