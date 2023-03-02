using System;
using UnityEngine;
using Urd.Services;
using Urd.Utils;

namespace Urd.Audio
{
    public class AudioFadeController : IDisposable
    {
        private ServiceHelper<IClockService> _clockService = new ServiceHelper<IClockService>();
        private AudioModel _audioModel;
        
        private float _timeStamp;
        private float _fadeFrom;
        private float _fadeTo;
        private float _fadeDuration;

        public event Action OnFinishFade; 
        
        public AudioFadeController(AudioModel audioModel, float fadeFrom, float fadeTo, float fadeDuration)
        {
            _audioModel = audioModel;

            _fadeFrom = fadeFrom;
            _fadeTo = fadeTo;
            _fadeDuration = fadeDuration;

            BeginFade();
        }

        private void BeginFade()
        {
            _timeStamp = 0;
            _audioModel.AudioSource.volume = 0;
            _clockService.Service.SuscribeToUpdate(FadeUpdate);
        }

        private void FadeUpdate(float deltaTime)
        {
            float fadeValue = Mathf.Lerp(_fadeFrom, _fadeTo, _timeStamp / _fadeDuration);
            _audioModel.AudioSource.volume = fadeValue;
            _timeStamp += deltaTime;
            if (_timeStamp >= _fadeDuration)
            {
                EndFade();
            }
        }

        private void EndFade()
        {
            OnFinishFade?.Invoke();
            _clockService.Service.UnSuscribeToUpdate(FadeUpdate);
            Dispose();
        }

        public void Dispose()
        {
            _audioModel = null;
            _clockService = null;
            OnFinishFade = null;
        }
    }
}