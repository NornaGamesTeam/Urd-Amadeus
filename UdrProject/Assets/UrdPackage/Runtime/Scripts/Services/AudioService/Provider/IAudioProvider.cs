using System;
using System.Collections.Generic;
using Urd.Audio;

namespace Urd.Services.Audio
{
    public interface IAudioProvider
    {
        void Init();
        public void GetAudioConfigData(Action<List<AudioConfigData>> audioConfigDataCallback);
    }
}