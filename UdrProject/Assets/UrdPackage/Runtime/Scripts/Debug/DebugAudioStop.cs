using UnityEngine;
using Urd.Audio;
using Urd.Services;
using Urd.Utils;

namespace Urd.DebugTools
{
    public class DebugAudioStop : DebugAbstract
    {
        private AudioModel _audioModel;

        public override void OnInputGetDown()
        {
            _audioModel = GetAudioModel();
            
            var audioService = StaticServiceLocator.Get<IAudioService>();
            audioService.Stop(_audioModel, OnAudioModelStop);
        }

        private void OnAudioModelStop()
        {
            Debug.Log("OnAudioModelStop");
            FindObjectOfType<DebugAudioPlay>().AudioModel = null;
        }

        private AudioModel GetAudioModel()
        {
            return FindObjectOfType<DebugAudioPlay>()?.AudioModel;
        }
    }
}