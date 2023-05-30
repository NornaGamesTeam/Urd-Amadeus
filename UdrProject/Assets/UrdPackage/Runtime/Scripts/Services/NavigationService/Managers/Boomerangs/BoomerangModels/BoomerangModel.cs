using MyBox;
using UnityEngine;
using Urd.Services.Navigation;

namespace Urd.Boomerang
{
    public class BoomerangModel : Navigable
    {
        public override string Id => BoomerangType.ToString();

        [field: SerializeField, ReadOnly]
        public BoomerangTypes BoomerangType { get; protected set; }
        
        [field: SerializeField]
        public float Duration { get; protected set; }

        public BoomerangModel(BoomerangTypes boomerangType) : this(boomerangType, 0){}
        public BoomerangModel(BoomerangTypes boomerangType, float duration)
        {
            BoomerangType = boomerangType;
            SetDuration(duration);
        }

        public void SetDuration(float duration)
        {
            Duration = duration;
        }
    }
}