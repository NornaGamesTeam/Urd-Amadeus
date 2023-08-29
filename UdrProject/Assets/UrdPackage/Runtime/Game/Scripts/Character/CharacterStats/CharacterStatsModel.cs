using System;
using System.Collections.Generic;
using MyBox;
using UnityEngine;
using Urd.Character.Skill;

namespace Urd.Character
{
    [Serializable]
    public class CharacterStatsModel
    {
        [field: SerializeField, MyBox.ReadOnly]
        public float CurrentHitPoints { get; private set; }
        [field: SerializeField, MyBox.ReadOnly]
        public float CurrentShield { get; private set; }
        [field: SerializeField, MyBox.ReadOnly]
        public float CurrentAttack { get; private set; }
        [field: SerializeField, MyBox.ReadOnly]
        public float CurrentDefense { get; private set; }
        [field: SerializeField, MyBox.ReadOnly]
        public float CurrentSpecialAttack { get; private set; }
        [field: SerializeField, MyBox.ReadOnly]
        public float CurrentSpecialDefense { get; private set; }
        [field: SerializeField, MyBox.ReadOnly]
        public float CurrentCriticChance { get; private set; }
        
        
        public float DefaultHitPoints => _characterModel.CharacterConfig.HitPoints;
        public float DefaultAttack =>_characterModel.CharacterConfig.Attack;
        public float DefaultDefense =>_characterModel.CharacterConfig.Defense;
        public float DefaultSpecialAttack =>_characterModel.CharacterConfig.SpecialAttack;
        public float DefaultSpecialDefense =>_characterModel.CharacterConfig.SpecialDefense;
        public float DefaultCriticChance =>_characterModel.CharacterConfig.CriticChance;
        
        public float MaxHitPoints => GetFinalStats(StatType.HitPoints);
        
        [field: SerializeField, MyBox.ReadOnly]
        public List<ElementModsInfo> Vulnerabilities { get; private set; }
        [field: SerializeField, MyBox.ReadOnly]
        public List<ElementModsInfo> Resistances { get; private set; }
        

        [field: SerializeField, ReadOnly]
        public ISkillModel HitSkillModel { get; private set; }
        
        public bool HasShield => CurrentShield > 0;
        public bool HasFullHitPoints => CurrentHitPoints == MaxHitPoints;
        public bool IsHit { get; private set; }

        public event Action OnShieldModified;
        public event Action<bool, Vector2, ISkillModel> OnIsHit;

        private CharacterModel _characterModel;
        
        public CharacterStatsModel(CharacterModel characterModel)
        {
            _characterModel = characterModel;

            CurrentHitPoints = MaxHitPoints;
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

        public bool TryHit(float hitDamage, Vector2 hitDirection, out ISkillModel hitSkillModel)
        {
            hitSkillModel = _characterModel.CharacterConfig.HitSkillConfig.Model;
            var finalDamage = hitDamage;

            if (CanHitOnlyShield(finalDamage))
            {
                CurrentShield -= finalDamage;
                OnShieldModified ?.Invoke();

                return false;
            }
            
            if (CurrentShield > 0)
            {
                finalDamage -= CurrentShield;
                CurrentShield = 0;
            }
            
            CurrentHitPoints -= finalDamage;
            SetIsHit(finalDamage > 0, hitDirection);
            return finalDamage > 0;
        }

        private bool CanHitOnlyShield(float hitDamage)
        {
            if (CurrentShield < 0)
            {
                return false;
            }

            if (CurrentShield < hitDamage)
            {
                return false;
            }

            return true;
        }
        
        public void SetIsHit(bool isHit, Vector2 hitDirection)
        {
            IsHit = isHit;
            OnIsHit?.Invoke(isHit, -hitDirection, _characterModel.CharacterConfig.HitSkillConfig.Model);
        }
        
        private float GetFinalStats(StatType statType)
        {
            var factorPerSkill = _characterModel.SkillSetModel.GetPassiveModificationFor(statType);
            switch (statType)
            {
                case StatType.HitPoints: return DefaultHitPoints * factorPerSkill;
                case StatType.Attack: return DefaultAttack * factorPerSkill; 
                case StatType.Defense: return DefaultDefense * factorPerSkill; 
                case StatType.SpecialAttack: return DefaultSpecialAttack * factorPerSkill; 
                case StatType.SpecialDefense: return DefaultSpecialDefense * factorPerSkill; 
                case StatType.CriticChance: return DefaultCriticChance * factorPerSkill; 
                case StatType.Shield: return CurrentShield * factorPerSkill; 
            }

            return 0;
        }
    }
}