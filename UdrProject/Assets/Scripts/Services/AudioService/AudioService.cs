using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Urd.Audio;
using Urd.Error;
using Urd.Services.Audio;
using Urd.Utils;

namespace Urd.Services
{
    public class AudioService : BaseService, IAudioService
    {
        private const string AUDIO_LABEL = "Audio";
        
        private IAudioProvider _audioProvider;

        private List<AudioConfigData> _audioConfigData;
        private List<AudioMixer> _audioMixers;

        private IAssetService _assetService;

        public override void Init()
        {
            base.Init();

            _assetService = StaticServiceLocator.Get<IAssetService>();
            
            LoadDefaultProvider();
            LoadAudioMixers();
        }

        private void LoadDefaultProvider()
        {
            SetProvider(new AudioProviderUnityResources());
        }
        
        public void SetProvider(IAudioProvider audioProvider)
        {
            _audioProvider = audioProvider;
            _audioProvider.GetAudioConfigData(OnGetAudioData);
        }
        private void LoadAudioMixers()
        {
            _assetService.LoadAssetByLabel<AudioMixer>(AUDIO_LABEL, OnAudioMixedLoaded);
        }

        private void OnAudioMixedLoaded(List<AudioMixer> audioMixers)
        {
            if (CanSetAsLoaded())
            {
                _audioMixers = audioMixers;
            }
        }

        private void OnGetAudioData(List<AudioConfigData> audioConfigData)
        {
            _audioConfigData = audioConfigData;
            if (CanSetAsLoaded())
            {
                SetAsLoaded();
            }
        }

        private bool TryGetAudioData(AudioTypes audioType, out AudioConfigData audioConfigData)
        {
            audioConfigData = _audioConfigData.Find(audioConfig => audioConfig.AudioType == audioType);
            return audioConfigData != null;
        }

        private bool CanSetAsLoaded()
        {
            return _audioMixers != null && _audioConfigData != null;
        }
        
        public AudioModel Play(AudioTypes audioType)
        {
            if (!TryGetAudioData(audioType, out var audioConfigData))
            {
                var error = new ErrorModel($"[AudioService] Error when try to get Audio of type {audioType}", 
                                           ErrorCode.Error_404_Not_Found);
                Debug.LogWarning(error);
                return null;
            }
            
            var audioModel = new AudioModel(audioConfigData);
            Play(audioModel);
            return audioModel;
        }

        public void Play(AudioModel audioModel)
        {
            
        }

        public void Pause(AudioModel audioModel, Action OnPauseCallback)
        {
            
        }

        public void Stop(AudioModel audioModel, Action OnStopCallback)
        {
            
        }
    }
}