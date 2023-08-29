using Urd.Character.Skill;
using Urd.Models;
using Urd.UI;

namespace Urd.Character
{
    public interface ICharacterModel
    {
        public UICharacterConfig UICharacterConfig { get; }
        public CharacterStatsModel CharacterStatsModel { get; }
        public CharacterMovementModel MovementModel { get; }
        public SkillSetModel SkillSetModel  { get; }
    }
}