using System;
using UnityEngine;
using Urd.Services.Navigation;
using Urd.WorldAreaTransition;

namespace Urd.View.WorldAreaTransition
{
    public class WorldAreaTransitionView<T> : WorldAreaTransitionViewNoModel, IWorldAreaTransitionView where T : WorldAreaTransitionModel
    {
        public GameObject GameObject => gameObject;
        public WorldAreaTransitionModel WorldAreaTransitionModel { get; private set; }
        public T Model => WorldAreaTransitionModel as T;
        public event Action OnClickOnClose;

        public void SetWorldAreaTransitionModel(WorldAreaTransitionModel worldAreaTransitionModel)
        {
            WorldAreaTransitionModel = worldAreaTransitionModel;

            worldAreaTransitionModel.OnStatusChanged += OnWorldAreaTransitionModelOnStatusChanged;
        }

        private void OnWorldAreaTransitionModelOnStatusChanged(NavigableStatus statusFrom, NavigableStatus statusTo)
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
