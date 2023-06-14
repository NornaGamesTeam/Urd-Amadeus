using Urd.Character.Skill;
using Urd.Models;
using Urd.UI;

namespace Urd.Character
{
    public interface ICharacterModel
    {
        public UICharacterConfig UICharacterConfig { get; }
        public CharacterHitPointsModel HitPointsModel { get; }
        public CharacterMovementModel CharacterMovement  { get; }
        public SkillSetModel SkillSetModel  { get; }
    }
}