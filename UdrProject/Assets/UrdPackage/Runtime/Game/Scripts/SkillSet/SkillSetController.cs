using System;
using UnityEngine;
using Urd.Character.Skill;

namespace Urd.Character
{
    [System.Serializable]
    public class SkillSetController : IDisposable
    {
        private CharacterModel _characterModel;

        private SkillSetModel _skillSetModel;
        public SkillSetController(CharacterModel characterModel)
        {
            _characterModel = characterModel;
            _skillSetModel = characterModel.SkillSetModel;

            _skillSetModel.OnSkillAction += OnSkillAction;
        }

        public void Dispose()
        {
            _skillSetModel.OnSkillAction -= OnSkillAction;
        }
        
        private void OnSkillAction(SkillModel skillModel)
        {
            Debug.Log(skillModel.Name);
        }

    }
}