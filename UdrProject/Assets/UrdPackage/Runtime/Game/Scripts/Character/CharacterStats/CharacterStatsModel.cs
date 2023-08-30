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

        
        public float DefaultHitPoints => _characterModel.CharacterConfig.HitPoints;
        public float DefaultAttack => _characterModel.CharacterConfig.Attack;
        public float DefaultDefense => _characterModel.CharacterConfig.Defense;
        public float DefaultSpecialAttack => _characterModel.CharacterConfig.SpecialAttack;
        public float DefaultSpecialDefense => _characterModel.CharacterConfig.SpecialDefense;
        public float DefaultCriticChance => _characterModel.CharacterConfig.CriticChance;
        
        public float MaxHitPoints => GetFinalStats(DefaultHitPoints, StatType.HitPoints);
        public float CurrentAttack => GetFinalStats(DefaultAttack, StatType.Attack);
        public float CurrentDefense => GetFinalStats(DefaultDefense, StatType.Defense);
        public float CurrentSpecialAttack => GetFinalStats(DefaultSpecialAttack, StatType.SpecialAttack);
        public float CurrentSpecialDefense => GetFinalStats(DefaultSpecialDefense, StatType.SpecialDefense);
        public float CurrentCriticChance => GetFinalStats(DefaultCriticChance, StatType.CriticChance);
        

        public float DefaultFireVulnerability 
            => _characterModel.CharacterConfig.Vulnerabilities
                              .Find(vulnerability =>
                                        vulnerability.Element == ElementType.Fire)
                              .Percentage;

        public float DefaultWaterVulnerability
            => _characterModel.CharacterConfig.Vulnerabilities
                              .Find(vulnerability =>
                                        vulnerability.Element == ElementType.Water)
                              .Percentage;

        public float DefaultAirVulnerability
            => _characterModel.CharacterConfig.Vulnerabilities
                              .Find(vulnerability =>
                                        vulnerability.Element == ElementType.Air)
                              .Percentage;

        public float DefaultEarthVulnerability
            => _characterModel.CharacterConfig.Vulnerabilities
                              .Find(vulnerability =>
                                        vulnerability.Element == ElementType.Earth)
                              .Percentage;

        public float DefaultLightVulnerability
            => _characterModel.CharacterConfig.Vulnerabilities
                              .Find(vulnerability =>
                                        vulnerability.Element == ElementType.Light)
                              .Percentage;

        public float DefaultDarkVulnerability
            => _characterModel.CharacterConfig.Vulnerabilities
                              .Find(vulnerability =>
                                        vulnerability.Element == ElementType.Dark)
                              .Percentage;

        public float CurrentFireVulnerability => GetFinalVulnerabilityRange(DefaultFireVulnerability, ElementType.Fire) ;
        public float CurrentWaterVulnerability => GetFinalVulnerabilityRange(DefaultWaterVulnerability, ElementType.Water);
        public float CurrentAirVulnerability => GetFinalVulnerabilityRange(DefaultAirVulnerability, ElementType.Air);
        public float CurrentEarthVulnerability => GetFinalVulnerabilityRange(DefaultEarthVulnerability, ElementType.Earth);
        public float CurrentLightVulnerability => GetFinalVulnerabilityRange(DefaultLightVulnerability, ElementType.Light);
        public float CurrentDarkVulnerability => GetFinalVulnerabilityRange(DefaultDarkVulnerability, ElementType.Dark);

        public float DefaultFireResistance 
            => _characterModel.CharacterConfig.Resistances
                              .Find(resistance =>
                                        resistance.Element == ElementType.Fire)
                              .Percentage;

        public float DefaultWaterResistance
            => _characterModel.CharacterConfig.Resistances
                              .Find(resistance =>
                                        resistance.Element == ElementType.Water)
                              .Percentage;

        public float DefaultAirResistance
            => _characterModel.CharacterConfig.Resistances
                              .Find(resistance =>
                                        resistance.Element == ElementType.Air)
                              .Percentage;

        public float DefaultEarthResistance
            => _characterModel.CharacterConfig.Resistances
                              .Find(resistance =>
                                        resistance.Element == ElementType.Earth)
                              .Percentage;

        public float DefaultLightResistance
            => _characterModel.CharacterConfig.Resistances
                              .Find(resistance =>
                                        resistance.Element == ElementType.Light)
                              .Percentage;

        public float DefaultDarkResistance
            => _characterModel.CharacterConfig.Resistances
                              .Find(resistance =>
                                        resistance.Element == ElementType.Dark)
                              .Percentage;

        public float CurrentFireResistance => GetFinalResistanceRange(DefaultFireResistance, ElementType.Fire);
        public float CurrentWaterResistance => GetFinalResistanceRange(DefaultWaterResistance, ElementType.Water);
        public float CurrentAirResistance => GetFinalResistanceRange(DefaultAirResistance, ElementType.Air);
        public float CurrentEarthResistance => GetFinalResistanceRange(DefaultEarthResistance, ElementType.Earth);
        public float CurrentLightResistance => GetFinalResistanceRange(DefaultLightResistance, ElementType.Light);
        public float CurrentDarkResistance => GetFinalResistanceRange(DefaultDarkResistance, ElementType.Dark);


        [field: SerializeField, ReadOnly] public ISkillModel HitSkillModel { get; private set; }

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
                OnShieldModified?.Invoke();

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

        private float GetFinalStats(float defaultValue, StatType statType)
        {
            var factorPerSkill = _characterModel.SkillSetModel.GetPassiveVulnerabilityFor(statType);
            return defaultValue * factorPerSkill;
        }

        private float GetFinalVulnerabilityRange(float defaultValue, ElementType elementType)
        {
            var factorPerSkill = _characterModel.SkillSetModel.GetPassiveVulnerabilityFor(elementType);
            return defaultValue * factorPerSkill;
        }
        
        private float GetFinalResistanceRange(float defaultValue, ElementType elementType)
        {
            var factorPerSkill = _characterModel.SkillSetModel.GetPassiveResistanceFor(elementType);
            return defaultValue * factorPerSkill;
        }
        
    }
}