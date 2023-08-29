using System;
using MyBox;
using UnityEngine;
using Urd.Character.Skill;

namespace Urd.Character
{
    [Serializable]
    public class CharacterHitPointsModel
    {
        private CharacterConfig _characterConfig;
        
        [field: SerializeField, ReadOnly]
        public float HitPoints { get; private set; }
        
        [field: SerializeField, ReadOnly]
        public float Shield { get; private set; }

        public ISkillModel HitSkillModel { get; private set; }

        public bool HasShield => Shield > 0;

        public CharacterHitPointsModel(CharacterConfig characterConfig)
        {
            _characterConfig = characterConfig;
            HitPoints = characterConfig.HitPoints;
            HitSkillModel = _characterConfig.HitSkillConfig.Model;
        }

        public bool TryHit(float hitDamage, Vector2 hitDirection, out float finalDamage)
        {
            finalDamage = hitDamage;
            if (CanHitOnlyShield(finalDamage))
            {
                Shield -= finalDamage;
                return false;
            }

            if (Shield > 0)
            {
                finalDamage -= Shield;
                Shield = 0;
            }
            
            HitPoints -= finalDamage;
            return true;
            
            return true;
        }

        private bool CanHitOnlyShield(float hitDamage)
        {
            if (Shield < 0)
            {
                return false;
            }

            if (Shield < hitDamage)
            {
                return false;
            }

            return true;
        }
    }
}