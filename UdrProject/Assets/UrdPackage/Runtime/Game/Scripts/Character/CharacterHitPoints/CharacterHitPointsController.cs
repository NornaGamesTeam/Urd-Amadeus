using System;
using UnityEngine;
using Urd.Boomerang;
using Urd.Services;
using Urd.Utils;

namespace Urd.Character
{
    public class CharacterHitPointsController : IDisposable
    {
        private CharacterModel _characterModel;

        private ServiceHelper<IClockService> _clockService = new();
        private ServiceHelper<INavigationService> _navigationService = new();

        public CharacterHitPointsController(CharacterModel characterModel)
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
            _characterModel.HitPointsModel.Hit(damage, hitDirection);
            _clockService.Service.AddDelayCall(_characterModel.HitPointsModel.HitSkillModel.Duration, OnFinishHit);

            ShowDamage();
        }

        private void ShowDamage()
        {
            BoomerangHitDamageModel hitDamageModel = _navigationService.Service.GetModel<BoomerangTypes, BoomerangHitDamageModel>(BoomerangTypes.HitDamage);
            Debug.Log(hitDamageModel);
            _navigationService.Service.Open(hitDamageModel);
        }

        private void OnFinishHit()
        {
            _characterModel.HitPointsModel.SetIsHit(false, Vector2.zero);
        }
    }
}