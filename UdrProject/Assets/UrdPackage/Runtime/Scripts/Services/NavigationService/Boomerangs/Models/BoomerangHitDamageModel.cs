using System;
using MyBox;
using UnityEngine;

namespace Urd.Boomerang
{
    [Serializable]
    public class BoomerangHitDamageModel : BoomerangModel
    {
        [field: SerializeField]
        public float Speed { get; private set; }
        
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