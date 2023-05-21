using System;

namespace Urd.Character.Skill
{
    public interface ISkillController : IDisposable
    {
        void Init(CharacterModel characterModel, ICharacterInput characterInput);
        void SetInput(ICharacterInput characterInput);
    }
}