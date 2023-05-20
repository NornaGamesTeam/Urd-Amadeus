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

        public void SetIsMeleeAttack(bool isMeleeAttack)
        {
            var meleeAttack = MeleeAttackModel;
            if (meleeAttack == null)
            {
                return;
            }

            meleeAttack.SetIsActive(isMeleeAttack);
            SetIsSkill(isMeleeAttack);
            if (isMeleeAttack)
            {
                OnSkillAction?.Invoke(meleeAttack);
            }
        }
        
        public void SetIsDodging(bool isDodging)
        {
            var dodgeSkill = DodgeSkillModel;
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
