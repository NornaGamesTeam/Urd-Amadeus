using System;
using UnityEngine;
using Urd.Audio;
using Urd.Services;
using Urd.Utils;

namespace Urd.DebugTools
{
    public class DebugAudioPlay : DebugAbstract
    {
        [SerializeField] 
        private AudioTypes _audioTypes;

        [field: SerializeField, MyBox.ReadOnly]
        public AudioModel AudioModel { get; set; }

        public override void OnInputGetDown()
        {
            var audioService = StaticServiceLocator.Get<IAudioService>();

            if (AudioModel == null)
            {
                AudioModel = audioService.Play(_audioTypes);
            }
            else
            {
                audioService.Play(AudioModel);
            }
        }
    }
}