using System;
using System.Collections.Generic;
using UnityEngine;
using Urd.Audio;

namespace Urd.Services.Audio
{
    [Serializable]
    public class AudioProviderUnityResources : IAudioProvider
    {
        public void Init() { }

        public void GetAudioConfigData(Action<List<AudioConfigData>> audioConfigDataCallback)
        {
            List<AudioConfigData> result = new ();
            var allAudioConfigs = Resources.LoadAll<AudioConfig>(String.Empty);
            for (int i = 0; i < allAudioConfigs.Length; i++)
            {
                result.AddRange(allAudioConfigs[i].AudioConfigData);
            }
            
            audioConfigDataCallback?.Invoke(result);
        }
    }
}