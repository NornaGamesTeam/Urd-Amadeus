using Urd.Character;

namespace Urd.UI
{
    public class UISkillDodge : UISkill
    {
        public override void SetCharacterModel(ICharacterModel characterModel)
        {
            base.SetCharacterModel(characterModel);
            
            SetSkill(characterModel.SkillSetModel.DodgeSkillModel);
        }
    }
}
