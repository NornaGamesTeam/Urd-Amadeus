using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Urd.Audio;
using Urd.Error;
using Urd.Services.Audio;

namespace Urd.Services
{
    public class AudioService : BaseService, IAudioService
    {
        private IAudioProvider _audioProvider;

        private List<AudioConfigData> _audioConfigData;
        private List<AudioMixer> _audioMixers;

        private IAssetService _assetService;

        public override void Init()
        {
            base.Init();

            LoadDefaultProvider();
            LoadAudioConfigs();
        }

        private void LoadDefaultProvider()
        {
            SetProvider(new AudioProviderUnityResources());
        }
        
        public void SetProvider(IAudioProvider audioProvider)
        {
            _audioProvider = audioProvider;
        }
        private void LoadAudioConfigs()
        {
            _audioProvider.GetAudioConfigData(OnGetAudioData);
        }

        private void OnGetAudioData(List<AudioConfigData> audioConfigData)
        {
            _audioConfigData = audioConfigData;
            SetAsLoaded();
        }
        
        private bool TryGetAudioData(AudioTypes audioType, out AudioConfigData audioConfigData)
        {
            audioConfigData = _audioConfigData.Find(audioConfig => audioConfig.AudioType == audioType);
            return audioConfigData != null;
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