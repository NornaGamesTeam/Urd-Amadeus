using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Urd.Services
{
    public class ClockService : BaseService, IClockService
    {
        public bool IsInPause { get; private set; }

        List<ClockServiceUpdateModel> _updateListeners = new List<ClockServiceUpdateModel>();
        List<ClockServiceDelayedCallModel> _delayedCalls = new List<ClockServiceDelayedCallModel>();

        private ICoroutineService _coroutineService;

        private bool _update;

        public override void Init()
        {
            base.Init();
            _coroutineService = ServiceLocatorService.Get<ICoroutineService>();

            _coroutineService.StartCoroutine(UpdateCoroutineCo());
        }

        public void SetPause(bool gamePaused)
        {
            IsInPause = gamePaused;
        }

        public void SuscribeToUpdate(Action<float> listener, bool pausable = true)
        {
            _updateListeners.Add(new ClockServiceUpdateModel(listener, pausable));
        }

        public void UnSuscribeToUpdate(Action<float> listenerToDelete)
        {
            var model = _updateListeners.Find(listener => listener.Listener == listenerToDelete);
            if(model != null)
            {
                _updateListeners.Remove(model);
            }
        }

        public void AddDelayCall(float delayTime, Action methodWhenFinish, bool pausable = true)
        {
            _delayedCalls.Add(new ClockServiceDelayedCallModel(delayTime, methodWhenFinish, pausable));
        }

        private IEnumerator UpdateCoroutineCo()
        {
            while (_update)
            {
                float deltaTime = Time.deltaTime;
                Update(deltaTime);
                yield return 0;
            }
        }

        public void Update(float deltaTime)
        {
            UpdateUpdates(deltaTime);
            UpdateDelayedCalls(deltaTime);
        }

        private void UpdateUpdates(float deltaTime)
        {
            for (int i = 0; i < _updateListeners.Count; i++)
            {
                if (!IsInPause || !_updateListeners[i].IsPausable)
                {
                    _updateListeners[i].CallListener(deltaTime);
                }
            }
        }

        private void UpdateDelayedCalls(float deltaTime)
        {
            for (int i = _delayedCalls.Count - 1; i >= 0; i--)
            {
                if (!IsInPause || !_delayedCalls[i].IsPausable)
                {
                    _delayedCalls[i].DeductTime(deltaTime);

                    if (_delayedCalls[i].IsExpired)
                    {
                        _delayedCalls[i].CallMethodWhenFinish();
                        _delayedCalls.RemoveAt(i);
                    }
                }
            }
        }

        protected class ClockServiceDelayedCallModel
        {
            public bool IsPausable { get; private set; }
            public float DelayTime { get; private set; }
            public float RemainingTime { get; private set; }
            public Action MethodWhenFinish { get; private set;  }

            public bool IsExpired => RemainingTime <= 0;

            public ClockServiceDelayedCallModel(float delayTime, Action methodWhenFinish, bool pausable)
            {
                DelayTime = delayTime;
                RemainingTime = DelayTime;
                MethodWhenFinish = methodWhenFinish;
                IsPausable = pausable;
            }

            public void CallMethodWhenFinish()
            {
                MethodWhenFinish?.Invoke();
            }

            public void DeductTime(float deltaTime)
            {
                RemainingTime -= deltaTime;
            }
        }

        protected class ClockServiceUpdateModel
        {
            public bool IsPausable { get; private set; }
            
            public Action<float> Listener { get; private set; }

            public ClockServiceUpdateModel(Action<float> newListener) : this(newListener, true) { }

            public ClockServiceUpdateModel(Action<float> newListener, bool isPausable)
            {
                Listener = newListener;
                IsPausable = isPausable;
            }

            public void CallListener(float deltaTime)
            {
                Listener.Invoke(deltaTime);
            }
        }
    }
}