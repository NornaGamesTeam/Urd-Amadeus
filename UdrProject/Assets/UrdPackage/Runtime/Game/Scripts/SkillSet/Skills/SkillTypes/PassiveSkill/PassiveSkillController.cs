using System;

namespace Urd.Character.Skill
{
    [Serializable] 
    public class PassiveSkillController : SkillController<PassiveSkillModel>
    {
        public override void Init(ISkillModel skillModel, ICharacterModel characterModel,
            ICharacterInput characterInput)
        {
            base.Init(skillModel, characterModel, characterInput);
            
            // TODO
            //ShowEffect
        }

        public override void Dispose()
        {
            // TODO
            // Dissable Effect
            base.Dispose();
        }
    }
}