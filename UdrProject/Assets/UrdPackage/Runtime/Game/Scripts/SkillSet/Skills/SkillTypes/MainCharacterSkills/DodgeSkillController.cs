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

            characterInput.OnIsDodgingChanged += OnIsDodgingChanged;
        }

        public override void Dispose()
        {
            base.Dispose();
            _characterInput.OnIsDodgingChanged -= OnIsDodgingChanged;
        }

        private void OnIsDodgingChanged(bool isDodging)
        {
            if (isDodging)
            {
                Debug.Log("isDodging:{isDodging}");
            }
            
            SetIsDodging(isDodging);
        }

        private void SetIsDodging(bool isDodging)
        {
            if (_isDodging == isDodging)
            {
                return;
            }

            _isDodging = isDodging;
            _characterModel.SkillSetModel.SetIsDodging(_isDodging);

            if (isDodging)
            {
                StaticServiceLocator.Get<IClockService>().AddDelayCall(
                    _characterModel.SkillSetModel.DodgeSkill.Duration,
                    OnFinishDodge);
            }
        }

        private void OnFinishDodge()
        {
            SetIsDodging(false);
        }
    }
}