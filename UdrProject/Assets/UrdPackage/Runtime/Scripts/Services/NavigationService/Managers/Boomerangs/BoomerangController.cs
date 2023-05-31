using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Urd.Services;
using Urd.Services.Navigation;
using Urd.Utils;
using Urd.View.Boomerang;

namespace Urd.Boomerang
{
    public abstract class BoomerangController<TBoomerangModel> : IBoomerangController 
        where TBoomerangModel : BoomerangModel
    {
        [field: SerializeField]
        public virtual BoomerangBodyView BoomerangDefaultBody { get; protected set; }
        
        public BoomerangBodyView BoomerangBody { get; protected set; }
        public TBoomerangModel BoomerangModel => BoomerangBody?.BoomerangModel as TBoomerangModel;

        protected ServiceHelper<IClockService> _clockService = new();
        private Sequence _tweenSequence;

        public void SetBoomerangBody(BoomerangBodyView boomerangBody)
        {
            BoomerangBody = boomerangBody;
            BoomerangBody.BoomerangView.OnClickOnClose += Close;
        }

        public virtual void Open()
        {
            BoomerangBody.Open();
            BoomerangModel.ChangeStatus(NavigableStatus.Opening);

            if (BoomerangModel.FadeInTime > 0)
            {
                AnimateFadeTo(1, BoomerangModel.FadeInTime);
            }
            StaticServiceLocator.Get<IClockService>().AddDelayCall(BoomerangModel.FadeInTime, OnIdle);
        }
        
        protected virtual void OnIdle()
        {
            BoomerangModel.ChangeStatus(NavigableStatus.Idle);
            BoomerangBody.OnIdle();
            _clockService.Service.AddDelayCall(BoomerangModel.Duration, AutoClose);
        }

        private void AutoClose()
        {
            StaticServiceLocator.Get<INavigationService>().Close(BoomerangModel);
        }

        public virtual void Close()
        {
            if (BoomerangModel.FadeOutTime > 0)
            {
                AnimateFadeTo(0, BoomerangModel.FadeOutTime);
            }
            BoomerangModel.ChangeStatus(NavigableStatus.Closing);
            BoomerangBody.Close();
            StaticServiceLocator.Get<IClockService>().AddDelayCall(BoomerangModel.FadeOutTime, OnClose);
        }

        protected virtual void OnClose()
        {
            BoomerangModel.ChangeStatus(NavigableStatus.Closed);
            
            BoomerangBody.BoomerangView.OnClickOnClose -= Close;
            _tweenSequence.Kill();
        }
        
        private void AnimateFadeTo(int alphaValue, float duration)
        {
            _tweenSequence?.Complete();
            _tweenSequence = DOTween.Sequence();
            var spriteRenderers = BoomerangBody.GetComponentsInChildren<SpriteRenderer>();
            foreach (var spriteRenderer in spriteRenderers)
            {
                _tweenSequence.Join(spriteRenderer.DOFade(alphaValue, duration));
            }
            
            var images = BoomerangBody.GetComponentsInChildren<Image>();
            foreach (var image in images)
            {
                _tweenSequence.Join(image.DOFade(alphaValue, duration));
            }
            
            var textMeshes = BoomerangBody.GetComponentsInChildren<TMP_Text>();
            foreach (var textMesh in textMeshes)
            {
                _tweenSequence.Join(textMesh.DOFade(alphaValue, duration));
            }
        }
    }
}