using System;
using Urd.Game.SkillTrees;

namespace Urd.Character.Skill
{
    [Serializable]
    public class SkillModel
    {
        private SkillConfig _skillConfig;

        public string Name => _skillConfig.Name;
        public int LevelToUnlock => _skillConfig.LevelToUnlock;
        public SkillType Type => _skillConfig.Type;
        public float Duration => _skillConfig.Duration;

        public SkillModel(SkillConfig skillConfig)
        {
            _skillConfig = skillConfig;
        }
    }
}