using System;
using UnityEngine;
using Urd.Boomerang;
using Urd.Services;
using Urd.Utils;

namespace Urd.Character
{
    public class CharacterStatsController : IDisposable
    {
        private ICharacterModel _characterModel;

        private ServiceHelper<IClockService> _clockService = new();
        private ServiceHelper<INavigationService> _navigationService = new();

        public CharacterStatsController(ICharacterModel characterModel)
        {
            _characterModel = characterModel;
            Init();
        }

        private void Init()
        {
            
        }


        public void Dispose()
        {
            
        }

        public void Hit(float damage, Vector2 hitDirection)
        {
            if (_characterModel.CharacterStatsModel.TryHit(damage, hitDirection, out var hitSkillModel))
            {
                _clockService.Service.AddDelayCall(hitSkillModel.Duration, OnFinishHit);
                ShowDamage(damage, hitDirection);
            }
        }

        private void ShowDamage(float damage, Vector2 hitDirection)
        {
            BoomerangHitDamageModel hitDamageModel = _navigationService.Service.GetModel<BoomerangTypes, BoomerangHitDamageModel>(BoomerangTypes.HitDamage);
            hitDamageModel.SetDamage(damage);
            hitDamageModel.SetOriginPoint(_characterModel.MovementModel.PhysicPosition);
            _navigationService.Service.Open(hitDamageModel);
        }

        private void OnFinishHit()
        {
            _characterModel.CharacterStatsModel.SetIsHit(false, Vector2.zero);
        }
    }
}