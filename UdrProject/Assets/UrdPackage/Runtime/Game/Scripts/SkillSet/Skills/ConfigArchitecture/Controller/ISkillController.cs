using System;

namespace Urd.Character.Skill
{
    public interface ISkillController : IDisposable
    {
        void Init(ISkillModel defaultSkill, ICharacterModel characterModel, ICharacterInput characterInput);
        void SetInput(ICharacterInput characterInput);
    }
}