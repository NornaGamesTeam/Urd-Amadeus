using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Urd.Audio
{
    [CreateAssetMenu(fileName = "AudioConfig", menuName = "Urd/NewAudioConfig", order = 1)]
    public class AudioConfig : ScriptableObject
    {
        [field: SerializeField]
        public List<AudioMixer> AudioMixers { get; private set; }
    
        [field: SerializeField]
        public List<AudioConfigData> AudioConfigData { get; private set; }
    }
}