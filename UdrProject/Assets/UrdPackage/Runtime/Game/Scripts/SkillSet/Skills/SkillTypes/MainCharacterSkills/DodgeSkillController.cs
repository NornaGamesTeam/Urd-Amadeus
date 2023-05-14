using System;
using UnityEngine;
using Urd.Services;
using Urd.Utils;

namespace Urd.Character.Skill
{
    [Serializable] 
    public class DodgeSkillController : SkillController
    {
        private bool _isDodging;
        public override void Init(CharacterModel characterModel, ICharacterInput characterInput)
        {
            base.Init(characterModel, characterInput);
            
            characterInput.OnIsDodgingChanged += OnIsDodgingChanged
        }

        private void OnIsDodgingChanged(bool isDodging)
        {
            if (isDodging)
            {
                Debug.Log("isDodging:{isDodging}");
            }


            if (_isDodging == isDodging)
            {
                return;
            }


            _isDodging = isDodging;
            if (!isDodging)
            {
                _characterModel.SkillSetModel.SetIsDodging(_isDodging);
                StaticServiceLocator.Get<IClockService>().AddDelayCall(_characterModel.SkillSetModel.DodgeSkill);
            }
        }
    }
}