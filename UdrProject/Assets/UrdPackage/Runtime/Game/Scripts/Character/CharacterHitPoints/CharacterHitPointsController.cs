using System;
using UnityEngine;
using Urd.Services;
using Urd.Utils;

namespace Urd.Character
{
    public class CharacterHitPointsController : IDisposable
    {
        private CharacterModel _characterModel;

        private ServiceHelper<IClockService> _clockService = new();
        
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
        }

        private void OnFinishHit()
        {
            _characterModel.HitPointsModel.SetIsHit(false, Vector2.zero);
        }
    }
}