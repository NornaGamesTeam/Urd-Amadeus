using System;

namespace Urd.Services
{
    public interface IClockService : IBaseService
    {
        void SuscribeToUpdate(Action<float> listener, bool pausable = true);

        void UnSuscribeToUpdate(Action<float> listener);

        void SetPause(bool gamePaused);

        void AddDelayCall(float delayTime, Action methodWhenFinish, bool pausable = true);

        /// <summary>
        /// Method for Test propose, do not use!!
        /// </summary>
        void Update(float deltaTime);
    }
}