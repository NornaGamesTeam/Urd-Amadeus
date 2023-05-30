using System;
using MyBox;
using UnityEngine;
using Urd.Boomerang;
using Urd.Services.Navigation;

namespace Urd.View.Boomerang
{
    [Serializable]
    public abstract class BoomerangView<T> : BoomerangViewNoModel, IBoomerangView where T : BoomerangModel
    {
        public GameObject GameObject => gameObject;
        [field: SerializeField, ReadOnly]
        public BoomerangModel BoomerangModel { get; private set; }
        public T Model => BoomerangModel as T;
        public event Action OnClickOnClose;

        public void SetBoomerangModel(BoomerangModel boomerangModel)
        {
            BoomerangModel = boomerangModel;


            boomerangModel.OnStatusChanged += OnBoomerangModelStatusChanged;
        }

        private void OnBoomerangModelStatusChanged(NavigableStatus statusFrom, NavigableStatus statusTo)
        {
            switch (statusTo)
            {
                case NavigableStatus.Opening: OnBeginOpen();
                    break;
                case NavigableStatus.Idle: OnIdle();
                    break;
                case NavigableStatus.Closing: OnBeginClose();
                    break;
                case NavigableStatus.Closed: OnClose();
                    break;
                case NavigableStatus.Destroyed: OnDestroy();
                    break;
            }
        }

        public virtual void Close()
        {
            OnClickOnClose?.Invoke();
        }
        protected virtual void OnBeginOpen() { }        
        protected virtual void OnIdle() { }
        protected virtual void OnBeginClose() { }
        protected virtual void OnClose() { }
        protected virtual void OnDestroy() { }
    }
}
