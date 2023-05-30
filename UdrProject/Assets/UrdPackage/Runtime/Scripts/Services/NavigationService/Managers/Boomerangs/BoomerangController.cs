using UnityEngine;
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

        public void SetBoomerangBody(BoomerangBodyView boomerangBody)
        {
            BoomerangBody = boomerangBody;
            BoomerangBody.BoomerangView.OnClickOnClose += Close;
        }

        public virtual void Open()
        {
            BoomerangBody.Open();
            BoomerangModel.ChangeStatus(NavigableStatus.Opening);
            
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

            BoomerangModel.ChangeStatus(NavigableStatus.Closing);
            BoomerangBody.Close();
            StaticServiceLocator.Get<IClockService>().AddDelayCall(BoomerangModel.FadeOutTime, OnClose);
        }

        protected virtual void OnClose()
        {
            BoomerangModel.ChangeStatus(NavigableStatus.Closed);
            
            BoomerangBody.BoomerangView.OnClickOnClose -= Close;
        }
    }
}