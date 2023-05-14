using System;

namespace Urd.Character.Skill
{
    [Serializable] 
    public abstract class SkillController : ISkillController
    {
        protected CharacterModel _characterModel;
        protected ICharacterInput _characterInput;

        public virtual void Init(CharacterModel characterModel, ICharacterInput characterInput)
        {
            _characterModel = characterModel;
            SetInput(characterInput);
        }

        public void SetInput(ICharacterInput characterInput)
        {
            _characterInput = characterInput;
        }

        public virtual void Dispose()
        {
            _characterInput?.Dispose();
        }
    }
}