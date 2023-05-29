using System;
using Urd.Services.Physics;

namespace Urd.Character
{
    public class CharacterHitPointsModel
    {
        private CharacterConfig _characterConfig;
        public CharacterHitPointsModel(CharacterConfig characterConfig)
        {
            _characterConfig = characterConfig;
        }

        public float MaxHitPoints => _characterConfig.HitPoints;
        public float HitPoints { get; private set; }
        
        public event Action<bool> OnIsHit;

        public void Hit(float damage)
        {
            if (damage <= 0)
            {
                return;
            }
            
            HitPoints -= damage;
            SetIsHit(true);
        }

        public void SetIsHit(bool isHit)
        {
            OnIsHit?.Invoke(isHit);
        }
    }
}