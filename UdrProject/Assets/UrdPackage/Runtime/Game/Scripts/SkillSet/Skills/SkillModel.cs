using System;
using Urd.Game.SkillTrees;

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
        public CharacterAnimParameters AnimParameter => _skillConfig?.AnimParameter ?? CharacterAnimParameters.None;
        
        public void SetConfig(SkillConfig skillConfig)
        {
            _skillConfig = skillConfig as TSkill;
        }
    }
}