using System;
using UnityEngine;
using Urd.Timer;

namespace Urd.Services
{
    public interface IClockService : IBaseService
    {
        bool IsInPause { get; }
        float DeltaTime { get; }
        void SubscribeToUpdate(Action<float> listener, bool pausable = true);
        void UnSubscribeToUpdate(Action<float> listener);
        void SubscribeToLateUpdate(Action<float> listener, bool pausable = true);
        void UnSubscribeToLateUpdate(Action<float> listener);

        void SetPause(bool gamePaused);

        TimerModel AddDelayCall(float duration, Action finishCallback, bool pausable = true);

        /// <summary>
        /// Method for Test propose, do not use!!
        /// </summary>
        void __TestUpdate(float deltaTime);
    }
}