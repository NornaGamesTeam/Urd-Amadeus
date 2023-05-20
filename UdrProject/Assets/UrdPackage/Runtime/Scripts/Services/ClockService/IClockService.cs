using System;
using UnityEngine;

namespace Urd.Services
{
    public interface IClockService : IBaseService
    {
        bool IsInPause { get; }
        float DeltaTime { get; }
        void SubscribeToUpdate(Action<float> listener, bool pausable = true);

        void UnSubscribeToUpdate(Action<float> listener);

        void SetPause(bool gamePaused);

        void AddDelayCall(float duration, Action finishCallback, bool pausable = true);

        /// <summary>
        /// Method for Test propose, do not use!!
        /// </summary>
        void __TestUpdate(float deltaTime);
    }
}