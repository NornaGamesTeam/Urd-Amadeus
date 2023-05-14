using System;
using Urd.Game.SkillTrees;

namespace Urd.Character.Skill
{
    public interface ISkillModel
    {
        public string Name { get; }
        public int LevelToUnlock => _skillConfig.LevelToUnlock;
        public SkillType Type => _skillConfig.Type;
        public ISkillController Controller => _skillConfig.Controller;
        public float Duration => _skillConfig.Duration;
        public CharacterAnimParameters AnimParameter => _skillConfig.AnimParameter;
        
        public SkillModel(TSkill skillConfig)
        {
            _skillConfig = skillConfig;
        }
    }
}