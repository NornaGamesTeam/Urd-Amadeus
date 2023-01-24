using System;
using UnityEngine;
using Urd.Boomerang;
using Urd.Services;
using Urd.Services.Navigation;
using Urd.Utils;

namespace Urd.View.Boomerang
{
    public class BoomerangBodyView : MonoBehaviour
    {
        public IBoomerangView BoomerangView { get; private set; }
        public BoomerangModel BoomerangModel => BoomerangView?.BoomerangModel;
        
        [field: SerializeField]
        public Transform Container { get; private set; }

        public void SetBoomerangView(IBoomerangView boomerangView)
        {
            BoomerangView = boomerangView;

            boomerangView.OnClickOnClose += Close;
        }

        public void Open()
        {
            BoomerangModel.ChangeStatus(NavigableStatus.Opening);
            OnBeginOpen();
        }
        
        protected virtual void OnBeginOpen()
        {
            // TODO: The opening animation
            StaticServiceLocator.Get<IClockService>().AddDelayCall(0.1f, OnIdle);
        }

        protected virtual void OnIdle()
        {
            BoomerangModel.ChangeStatus(NavigableStatus.Idle);
            
            StaticServiceLocator.Get<IClockService>().AddDelayCall(BoomerangModel.Duration, Close);
        }
        
        public void Close()
        {
            if (!BoomerangModel.IsClosingOrDestroyed)
            {
                BoomerangModel.ChangeStatus(NavigableStatus.Closing);
                StaticServiceLocator.Get<INavigationService>().Close(BoomerangModel);
                OnBeginClose();
            }
        }
        
        public virtual void OnBeginClose()
        {
            // TODO  close Animation
            StaticServiceLocator.Get<IClockService>().AddDelayCall(0.1f, OnClose);
        }

        protected virtual void OnClose()
        {
            BoomerangModel.ChangeStatus(NavigableStatus.Closed);
        }

        private void OnDestroy()
        {
            if (BoomerangView != null)
            {
                BoomerangView.OnClickOnClose -= Close;
            }
        }
    }
}