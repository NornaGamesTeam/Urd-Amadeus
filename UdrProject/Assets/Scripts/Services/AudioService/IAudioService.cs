using System;
using Urd.Audio;
using Urd.Services.Audio;

namespace Urd.Services
{
    public interface IAudioService : IBaseService
    {
        void SetProvider(IAudioProvider audioProvider);

        AudioModel Play(AudioTypes audioTypes);
        void Play(AudioModel audioModel);
        void Pause(AudioModel audioModel, Action OnPauseCallback);
        void Stop(AudioModel audioModel, Action OnStopCallback);
    }
}