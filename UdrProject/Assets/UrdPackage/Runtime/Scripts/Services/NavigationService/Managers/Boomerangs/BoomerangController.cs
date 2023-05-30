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
        public BoomerangModel BoomerangModel => BoomerangBody?.BoomerangModel;

        private ServiceHelper<IClockService> _clockService = new();

        public void SetBoomerangBody(BoomerangBodyView boomerangBody)
        {
            BoomerangBody = boomerangBody;
            BoomerangBody.BoomerangView.OnClickOnClose += Close;
        }

        public void Open()
        {
            BoomerangBody.Open();
            BoomerangModel.ChangeStatus(NavigableStatus.Opening);
            
            StaticServiceLocator.Get<IClockService>().AddDelayCall(0.1f, OnIdle);
        }

        private void OnIdle()
        {
            BoomerangModel.ChangeStatus(NavigableStatus.Idle);
            BoomerangBody.OnIdle();
            _clockService.Service.AddDelayCall(BoomerangModel.Duration, Close);
        }

        public void Close()
        {
            BoomerangModel.ChangeStatus(NavigableStatus.Closing);
            BoomerangBody.Close();
            StaticServiceLocator.Get<IClockService>().AddDelayCall(0.1f, OnClose);
        }

        private void OnClose()
        {
            BoomerangModel.ChangeStatus(NavigableStatus.Closed);
            
            BoomerangBody.BoomerangView.OnClickOnClose -= Close;
        }
    }
}