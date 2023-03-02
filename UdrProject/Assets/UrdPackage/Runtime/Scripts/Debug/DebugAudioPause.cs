using UnityEngine;
using Urd.Audio;
using Urd.Services;
using Urd.Utils;

namespace Urd.DebugTools
{
    public class DebugAudioPause : DebugAbstract
    {
        public AudioModel _audioModel;

        public override void OnInputGetDown()
        {
            _audioModel = GetAudioModel();
            
            var audioService = StaticServiceLocator.Get<IAudioService>();
            audioService.Pause(_audioModel, OnAudioModelPaused);
        }

        private void OnAudioModelPaused()
        {
            Debug.Log("OnAudioModelPaused");
        }

        private AudioModel GetAudioModel()
        {
            return FindObjectOfType<DebugAudioPlay>()?.AudioModel;
        }
    }
}