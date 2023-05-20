using System;
using System.Collections.Generic;
using UnityEngine;
using Urd.Character.Skill;

namespace Urd.Character
{
    [System.Serializable]
    public class SkillSetController : IDisposable
    {
        private CharacterModel _characterModel;
        [SerializeField]
        private SkillSetModel _skillSetModel;
        
        private ICharacterInput _characterInput;

        private List<ISkillController> _usableSkills = new List<ISkillController>(); 

        public SkillSetController(CharacterModel characterModel, ICharacterInput characterInput)
        {
            _characterModel = characterModel;
            _skillSetModel = characterModel.SkillSetModel;
            _characterInput = characterInput;

            LoadUsableSkills();
            _skillSetModel.OnSkillAction += OnSkillAction;
        }

        private void LoadUsableSkills()
        {
            for (int i = 0; i < _skillSetModel.DefaultSkills.Count; i++)
            {
                var controller = _skillSetModel.DefaultSkills[i].Controller;
                controller.Init(_characterModel, _characterInput);
                _usableSkills.Add(controller);
            }
        }

        public void Dispose()
        {
            _skillSetModel.OnSkillAction -= OnSkillAction;
        }
        
        private void OnSkillAction(ISkillModel skillModel)
        {
            Debug.Log(skillModel.Name);
        }
    }
}