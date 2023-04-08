using System;
using System.Collections.Generic;
using Urd.Audio;
using Urd.Utils;

namespace Urd.Services.Audio
{
    [Serializable]
    public class AudioProviderRemoteConfig : IAudioProvider
    {
        private ServiceHelper<IRemoteConfigurationService> _remoteConfig;

        public void Init()
        {
            
        }

        public void GetAudioConfigData(Action<List<AudioConfigData>> audioConfigDataCallback)
        {
            if (!_remoteConfig.HasService)
            {
                _remoteConfig.OnInitialize += () => GetAudioConfigData(audioConfigDataCallback);
                return;
            }
            
            GetAudioConfigDataInternal(audioConfigDataCallback);
        }

        private void GetAudioConfigDataInternal(Action<List<AudioConfigData>> audioConfigDataCallback)
        {
           
        }
    }
}