using System;
using Urd.Game.SkillTrees;
using Urd.Timer;

namespace Urd.Character.Skill
{
    [Serializable]
    public class SkillModel<TSkill> : ISkillModel 
        where TSkill : SkillConfig
    {
        protected TSkill _skillConfig;

        public string Name => _skillConfig?.Name;
        public int LevelToUnlock => _skillConfig?.LevelToUnlock ?? 0;
        public SkillType Type => _skillConfig?.Type ?? SkillType.None;
        public ISkillController Controller => _skillConfig?.Controller;
        public float Duration => _skillConfig?.Duration ?? 0f;
        public float CoolDown => _skillConfig?.CoolDown ?? 0f;
        public ISkillAnimationModel SkillAnimationModel => _skillConfig?.SkillAnimationModel;
        public bool IsActive { get; private set; }
        public TimerModel TimerModel { get; private set; }

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