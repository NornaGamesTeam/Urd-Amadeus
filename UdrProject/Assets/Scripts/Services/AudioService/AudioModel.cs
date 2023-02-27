using UnityEngine;

namespace Urd.Audio
{
    public class AudioModel
    {
        public Transform AudioOrigin { get; private set; }
        public bool HasSoundOrigin => AudioOrigin != null;
        
        public AudioSource AudioSource { get; private set; } 
        public bool IsPlaying => AudioSource.isPlaying;
        public bool IsInPause { get; private set; }

        public AudioTypes AudioTypes => _audioConfigData.AudioType;
        public AudioClip AudioClip => _audioConfigData.Clip;
        public string AudioMixerName => _audioConfigData.AudioMixerName;
        public float Volume  => _audioConfigData.Volume;
        public float FadeIn  => _audioConfigData.FadeIn;
        public float PauseFadeIn  => _audioConfigData.PauseFadeIn;
        public float PauseFadeOut  => _audioConfigData.PauseFadeOut;
        public float FadeOut  => _audioConfigData.FadeOut;
        public bool IsLoopable => _audioConfigData.IsLoopable;

        private AudioConfigData _audioConfigData;
        
        public AudioModel(AudioConfigData audioConfigData)
        {
            _audioConfigData = audioConfigData;
        }

        public void SetSoundOrigin(Transform soundOrigin)
        {
            AudioOrigin = soundOrigin;
        }

        public void SetAudioSource(AudioSource audioSource)
        {
            AudioSource = audioSource;
        }

        public void SetAsPaused(bool paused)
        {
            IsInPause = paused;
        }
    }
}