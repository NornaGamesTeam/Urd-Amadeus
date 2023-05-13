using Urd.Game.SkillTrees;

namespace Urd.Character.Skill
{
    [System.Serializable]
    public class SkillTreeModel
    {
        private SkillTreeConfig _skillTreeConfig;

        public SkillTreeModel(SkillTreeConfig skillTreeConfig)
        {
            _skillTreeConfig = skillTreeConfig;
        }
    }
}