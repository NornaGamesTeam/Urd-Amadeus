using System;
using UnityEngine;

namespace Urd.Timer
{
    [Serializable]
    public class TimerModel
    {
        public bool IsPausable { get; private set; } = true;
        [field: SerializeField]
        public float Duration { get; private set; }
        [field: SerializeField]
        public float RemainingTime { get; private set; }
        public Action FinishCallback { get; private set; }

        public bool IsInCooldown => RemainingTime > 0;
        public bool HasCooldown => Duration > 0;

        public TimerModel(float duration) : this(duration, null, true) { }
        public TimerModel(float duration, Action finishCallback) : this(duration, finishCallback, true) { }

        public TimerModel(float duration, Action finishCallback, bool isPausable)
        {
            Duration = duration;
            FinishCallback = finishCallback;
            IsPausable = isPausable;
        }

        private void CallFinishCallback()
        {
            FinishCallback?.Invoke();
        }

        public void BeginTimer(Action finishCallback)
        {
            RemainingTime = Duration;
            FinishCallback = finishCallback;
        }
        
        public void DeductTime(float deltaTime)
        {
            RemainingTime -= deltaTime;
            if (RemainingTime <= 0)
            {
                CallFinishCallback();
            }
        }
    }
}