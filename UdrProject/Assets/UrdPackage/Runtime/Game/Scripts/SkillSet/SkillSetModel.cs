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

        public Dictionary<SkillSlotType, ISkillModel> EquippedSkills { get; private set; } = new Dictionary<SkillSlotType, ISkillModel>();
        public List<ISkillModel> Skills  { get; private set; } = new List<ISkillModel>();
        public bool IsDoingSkill { get; private set; }
        
        public MeleeAttackModel MeleeAttackModel => DefaultSkills.Find(skill => skill is MeleeAttackModel) as MeleeAttackModel;
        public DodgeSkillModel DodgeSkillModel => DefaultSkills.Find(skill => skill is DodgeSkillModel) as DodgeSkillModel;

        
        public event Action<ISkillModel> OnSkillAction;
        public event Action<bool> OnIsDoingSkill;

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
            IsDoingSkill = isSkill;
            OnIsDoingSkill?.Invoke(IsDoingSkill);
        }
        
        public float GetPassiveVulnerabilityFor(StatType statType)
        {
            var passiveSkills = Skills.FindAll(skill => skill.Type == SkillType.Pasive);
            passiveSkills.AddRange(DefaultSkills.FindAll(skill => skill.Type == SkillType.Pasive));
            
            float factor = 1;
            for (int i = 0; i < passiveSkills.Count; i++)
            {
                var passiveSkillOfStatType = passiveSkills[i] as ChangeStatsPassiveSkillModel;
                if (passiveSkillOfStatType?.Stat == statType)
                {
                    factor += passiveSkillOfStatType.Factor;
                }
            }
            
            return factor;
        }

        public float GetPassiveVulnerabilityFor(ElementType elementType)
        {
            var passiveSkills = Skills.FindAll(skill => skill.Type == SkillType.Pasive);
            passiveSkills.AddRange(DefaultSkills.FindAll(skill => skill.Type == SkillType.Pasive));
            
            float factor = 1;
            for (int i = 0; i < passiveSkills.Count; i++)
            {
                var passiveSkillOfStatType = passiveSkills[i] as VulnerabilityPassiveSkillModel;
                if (passiveSkillOfStatType?.Element == elementType)
                {
                    factor += passiveSkillOfStatType.Factor;
                }
            }
            
            return factor;
        }
        
        public float GetPassiveResistanceFor(ElementType elementType)
        {
            var passiveSkills = Skills.FindAll(skill => skill.Type == SkillType.Pasive);
            passiveSkills.AddRange(DefaultSkills.FindAll(skill => skill.Type == SkillType.Pasive));
            
            float factor = 1;
            for (int i = 0; i < passiveSkills.Count; i++)
            {
                var passiveSkillOfStatType = passiveSkills[i] as ResistancePassiveSkillModel;
                if (passiveSkillOfStatType?.Element == elementType)
                {
                    factor += passiveSkillOfStatType.Factor;
                }
            }
            
            return factor;
        }
    }
}
