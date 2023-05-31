using System;
using DG.Tweening;
using UnityEngine;

namespace Urd.Boomerang
{
    [Serializable]
    public class BoomerangHitDamageController: BoomerangController<BoomerangHitDamageModel>
    {
        private Tween _tweenAnimation;
        
        public override void Open()
        {
            base.Open();

            InitAnim();
        }

        private void InitAnim()
        {
            BoomerangBody.transform.position = BoomerangModel.OriginPoint;
            
            _tweenAnimation = BoomerangBody.transform.DOMoveY(BoomerangModel.Speed * BoomerangModel.TotalDuration, BoomerangModel.TotalDuration)
                                           .SetEase(BoomerangModel.AnimationEase);
        }

        protected override void OnClose()
        {
            _tweenAnimation.Complete();
            
            base.OnClose();
        }
    }
}