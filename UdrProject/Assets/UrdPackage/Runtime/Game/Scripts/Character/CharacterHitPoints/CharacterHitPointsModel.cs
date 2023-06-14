using System;
using MyBox;
using UnityEngine;
using Urd.Character.Skill;
using Urd.Game.SkillTrees;

namespace Urd.Character
{
    [Serializable]
    public class CharacterHitPointsModel
    {
        private CharacterConfig _characterConfig;

        public float MaxHitPoints => _characterConfig.HitPoints;
        
        [field: SerializeField, ReadOnly]
        public float HitPoints { get; private set; }

        public ISkillModel HitSkillModel { get; private set; }

        public bool IsHit { get; private set; }

        public event Action<bool, Vector2, ISkillModel> OnIsHit;
        
        public bool IsFull => HitPoints >= MaxHitPoints;
        
        public CharacterHitPointsModel(CharacterConfig characterConfig)
        {
            _characterConfig = characterConfig;
            HitPoints = MaxHitPoints;
            HitSkillModel = _characterConfig.HitSkillConfig.Model;
        }
        
        public void Hit(float damage, Vector2 hitDirection)
        {
            HitPoints -= damage;
            SetIsHit(damage > 0, hitDirection);
        }

        public void SetIsHit(bool isHit, Vector2 hitDirection)
        {
            IsHit = isHit;
            OnIsHit?.Invoke(isHit, -hitDirection, HitSkillModel);
        }
    }
}