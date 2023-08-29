using System;
using System.Collections.Generic;
using UnityEngine;
using Urd.Character.Skill;

namespace Urd.Character
{
    [Serializable]
    public class CharacterStatsModel
    {
        private CharacterConfig _characterConfig;

        public float CurrentHitPoints { get; private set; }
        public float CurrentAttack { get; private set; }
        public float CurrentDefense { get; private set; }
        public float CurrentSpecialAttack { get; private set; }
        public float CurrentSpecialDefense { get; private set; }
        public float CurrentCriticChance { get; private set; }
        
        public float DefaultHitPoints => _characterConfig.HitPoints;
        public float DefaultAttack { get; private set; }
        public float DefaultDefense { get; private set; }
        public float DefaultSpecialAttack { get; private set; }
        public float DefaultSpecialDefense { get; private set; }
        public float DefaultCriticChance { get; private set; }
        
        public List<ElementModsInfo> Vulnerabilities { get; private set; }
        [field: SerializeField] 
        public List<ElementModsInfo> Resistances { get; private set; }
        
        public float CurrentShield { get; private set; }
        
        public bool IsHit { get; private set; }
        public bool HasFullHitPoints => _characterHitPointsModel.HasFullHitPoints;

        public event Action OnShieldModified;
        public event Action<bool, Vector2, ISkillModel> OnIsHit;

        private CharacterHitPointsModel _characterHitPointsModel;
            
        public CharacterStatsModel(CharacterConfig characterConfig)
        {
            _characterConfig = characterConfig;
            _characterHitPointsModel = new CharacterHitPointsModel(_characterConfig);
        }
        
        public float GetFinalDamage(ElementType damageType, float damageFactor)
        {
            if (damageType == ElementType.None)
            {
                return CurrentAttack * damageFactor;
            }
            else
            {
                return CurrentSpecialAttack * damageFactor;
            }
        }

        public bool TryHit(float damage, Vector2 hitDirection, out ISkillModel hitSkillModel)
        {
            bool hadShield = _characterHitPointsModel.HasShield;
            hitSkillModel = _characterConfig.HitSkillConfig.Model;
            bool hit = _characterHitPointsModel.TryHit(damage, hitDirection, out var finalDamage);
            if (!hit || hadShield)
            {
                OnShieldModified ?.Invoke();
            }
            
            SetIsHit(hit && finalDamage > 0, hitDirection);
            return hit && finalDamage > 0;
        }

        public void SetIsHit(bool isHit, Vector2 hitDirection)
        {
            IsHit = isHit;
            OnIsHit?.Invoke(isHit, -hitDirection, _characterConfig.HitSkillConfig.Model);
        }
    }
}