using Urd.Character;

namespace Urd.UI
{
    public class UISkillDodge : UISkill
    {
        public override void SetCharacterModel(CharacterModel characterModel)
        {
            base.SetCharacterModel(characterModel);
            
            SetSkill(characterModel.SkillSetModel.DodgeSkillModel);
        }
    }
}
