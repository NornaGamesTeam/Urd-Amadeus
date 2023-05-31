using System;
using DG.Tweening;
using MyBox;
using Newtonsoft.Json;
using UnityEngine;

namespace Urd.Boomerang
{
    [Serializable]
    public class BoomerangHitDamageModel : BoomerangModel
    {
        [field: Header("Hit Damage Model"), SerializeField]
        public float Speed { get; private set; }
        
        [field: SerializeField]
        public Color TextColor { get; private set; }
        [field: SerializeField]
        public Ease AnimationEase { get; private set; }
        
        [field: SerializeField, ReadOnly]
        public float Damage { get; private set; }
        [field: SerializeField, ReadOnly]
        public Vector2 OriginPoint { get; private set; }

        public BoomerangHitDamageModel() : base(BoomerangTypes.HitDamage)
        {
        }

        public BoomerangHitDamageModel(BoomerangHitDamageModel boomerangModel) : base(boomerangModel)
        {
            Speed = boomerangModel.Speed;
            TextColor = boomerangModel.TextColor;
            AnimationEase = boomerangModel.AnimationEase;
        }

        public void SetDamage(float newDamage)
        {
            Damage = newDamage;
        }

        public void SetOriginPoint(Vector2 originPoint)
        {
            OriginPoint = originPoint;
        }

      
    }
}