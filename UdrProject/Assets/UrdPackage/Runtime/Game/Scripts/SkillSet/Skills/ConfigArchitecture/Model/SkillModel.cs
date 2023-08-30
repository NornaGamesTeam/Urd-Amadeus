using System;
using UnityEngine;
using Urd.Game.SkillTrees;
using Urd.Timer;
using Urd.UI;

namespace Urd.Character.Skill
{
    [Serializable]
    public class SkillModel<TSkill> : ISkillModel 
        where TSkill : SkillConfig
    {
        protected TSkill _skillConfig;

        [field: SerializeField]
        public string Name { get; private set; }
        [field: Header("Lock Properties"), SerializeField]
        public int LevelToUnlock { get; private set; }
        [field: SerializeField]
        public virtual SkillType Type { get; protected set; }
        public ISkillController Controller => _skillConfig?.Controller;
        [field: SerializeField]
        public UISkillConfig UISkillConfig { get; protected set; }
        
        [field: Header("Skill Graphic"), SerializeReference, SubclassSelector]
        public ISkillAnimationModel SkillAnimationModel { get; protected set; }
        [field: Header("Skill Properties"), Header("Generic Properties"), SerializeField]
        public float CoolDown { get; protected set; }
        [field: SerializeField, ShowIf("Type", SkillType.Active)]
        public virtual float Duration { get; protected set; }
        public TimerModel TimerModel { get; private set; }
        
        public bool IsActive { get; private set; }

        public SkillModel() { }
        
        public void SetConfig(SkillConfig skillConfig)
        {
            _skillConfig = skillConfig as TSkill;
            Init();
        }

        private void Init()
        {
            TimerModel = new TimerModel(CoolDown);
        }

        public void SetIsActive(bool isActive)
        {
            IsActive = isActive;
        }
    }
}