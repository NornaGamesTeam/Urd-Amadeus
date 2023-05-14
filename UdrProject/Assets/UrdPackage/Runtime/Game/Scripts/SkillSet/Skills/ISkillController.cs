using System;

namespace Urd.Character.Skill
{
    public interface ISkillController : IDisposable
    {
        void Init(ICharacterInput characterInput);
        void SetInput(ICharacterInput characterInput);
    }
}