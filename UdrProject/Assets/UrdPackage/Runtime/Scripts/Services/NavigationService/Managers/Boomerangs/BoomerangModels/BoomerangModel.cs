using DG.Tweening;
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
        
        [field: Header("Fade In"), SerializeField]
        public float FadeInTime { get; private set; }
        [field: SerializeField]
        public Ease FadeInEase { get; private set; }

        [field: Header("Duration"), SerializeField]
        public float Duration { get; protected set; }
        [field: Header("Fade Out"), SerializeField]
        public float FadeOutTime { get; private set; }
        [field: SerializeField]
        public Ease FadeOutEase { get; private set; }
        public float TotalDuration => FadeInTime + Duration + FadeOutTime;
        
        public BoomerangModel(BoomerangTypes boomerangType) : this(boomerangType, 0){}
        public BoomerangModel(BoomerangTypes boomerangType, float duration)
        {
            BoomerangType = boomerangType;
            SetDuration(duration);
        }

        public BoomerangModel(BoomerangModel boomerangModel)
        {
            BoomerangType = boomerangModel.BoomerangType;
            FadeInTime = boomerangModel.FadeInTime;
            Duration = boomerangModel.Duration;
            FadeOutTime = boomerangModel.FadeOutTime;
        }

        public void SetDuration(float duration)
        {
            Duration = duration;
        }
    }
}