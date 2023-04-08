using System;
using System.Collections;
using System.Collections.Generic;
using Castle.Components.DictionaryAdapter.Xml;
using UnityEngine;
using UnityEngine.Audio;
using Urd.Audio;
using Urd.Error;
using Urd.Services.Audio;
using Urd.Utils;

namespace Urd.Services
{
    [System.Serializable]
    public class AudioService : BaseService, IAudioService
    {
        private const string AUDIO_GAMEOBJECT_NAME = "AudioServiceView";
        private const string AUDIO_LABEL = "Audio";
        private const string DEFAULT_AUDIO_MIXER_NAME = "Master";
        
        [SerializeReference, SubclassSelector]
        private IAudioProvider _audioProvider;

        private List<AudioConfigData> _audioConfigData;
        private List<AudioMixer> _audioMixers;

        private IAssetService _assetService;

        private Transform _audioServiceView;

        private List<AudioModel> _audioPlaying = new List<AudioModel>();
        
        public override void Init()
        {
            base.Init();

            _assetService = StaticServiceLocator.Get<IAssetService>();

            CreateAudioServiceView();
            LoadAudioMixers();

            if (_audioProvider != null)
            {
                SetProvider(_audioProvider);
            }
        }

        private void CreateAudioServiceView()
        {
            _audioServiceView = new GameObject(AUDIO_GAMEOBJECT_NAME).AddComponent<AudioServiceView>().transform;

            if (Application.isPlaying)
            {
                GameObject.DontDestroyOnLoad(_audioServiceView.gameObject);
            }
        }

        public void SetProvider(IAudioProvider audioProvider)
        {
            _audioProvider = audioProvider;
            _audioProvider.GetAudioConfigData(OnGetAudioData);
        }
        private void LoadAudioMixers()
        {
            _assetService.LoadAssetByLabel<AudioMixer>(AUDIO_LABEL, OnAudioMixedLoaded);
        }

        private void OnAudioMixedLoaded(List<AudioMixer> audioMixers)
        {
            _audioMixers = audioMixers;
            if (CanSetAsLoaded())
            {
                SetAsLoaded();
            }
        }

        private void OnGetAudioData(List<AudioConfigData> audioConfigData)
        {
            _audioConfigData = audioConfigData;
            if (CanSetAsLoaded())
            {
                SetAsLoaded();
            }
        }

        private bool TryGetAudioData(AudioTypes audioType, out AudioConfigData audioConfigData)
        {
            audioConfigData = _audioConfigData.Find(audioConfig => audioConfig.AudioType == audioType);
            return audioConfigData != null;
        }

        private bool CanSetAsLoaded()
        {
            return _audioMixers != null && _audioConfigData != null;
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

            if (!TryGetAudioModelIfPaused(audioType, out AudioModel audioModel))
            {
                audioModel = new AudioModel(audioConfigData);
            }

            Play(audioModel);
            return audioModel;
        }

        public void Play(AudioModel audioModel)
        {
            if (!audioModel.IsInPause)
            {
                SetOrCreateAudioSource(audioModel);
                new AudioFadeController(audioModel, 0, audioModel.Volume, audioModel.FadeIn);
                audioModel.AudioSource.Play();
                _audioPlaying.Add(audioModel);
            }
            else
            {
                new AudioFadeController(audioModel, 0, audioModel.Volume, audioModel.PauseFadeIn);
                audioModel.SetAsPaused(false);
                audioModel.AudioSource.UnPause();
            }
        }

        public void Pause(AudioModel audioModel, Action onPauseCallback)
        {
            var audioFadeController = new AudioFadeController(audioModel, audioModel.AudioSource.volume, 0, audioModel.PauseFadeOut);
            audioFadeController.OnFinishFade += () => OnFinishPauseFade(audioModel, onPauseCallback);
        }

        private void OnFinishPauseFade(AudioModel audioModel, Action onPauseCallback)
        {
            audioModel.AudioSource.Pause();
            audioModel.SetAsPaused(true);
            onPauseCallback?.Invoke();
        }

        public void Stop(AudioModel audioModel, Action onStopCallback)
        {
            var audioFadeController = new AudioFadeController(audioModel, audioModel.AudioSource.volume, 0, audioModel.FadeOut);
            audioFadeController.OnFinishFade += () => OnFinishStopFade(audioModel, onStopCallback);
        }

        private void OnFinishStopFade(AudioModel audioModel, Action onStopCallback)
        {
            audioModel.AudioSource.Stop();
            onStopCallback?.Invoke();
            _audioPlaying.Remove(audioModel);
        }

        private void SetOrCreateAudioSource(AudioModel audioModel)
        {
            var audioSourceOrigin = audioModel.AudioOrigin != null? audioModel.AudioOrigin : _audioServiceView;
            var audioSource = audioSourceOrigin.gameObject.AddComponent<AudioSource>();
            audioSource.clip = audioModel.AudioClip;
            audioSource.loop = audioModel.IsLoopable;
            audioSource.outputAudioMixerGroup = GetAudioMixer(audioModel);
            audioModel.SetAudioSource(audioSource);
        }
        
        private bool TryGetAudioModelIfPaused(AudioTypes audioType, out AudioModel audioModel)
        {
            audioModel = _audioPlaying.Find(audioPlaying => audioPlaying.AudioTypes == audioType && audioPlaying.IsInPause);
            return audioModel != null;
        }
        
        private AudioMixerGroup GetAudioMixer(AudioModel audioModel)
        {
            if (!string.IsNullOrEmpty(audioModel.AudioMixerName) 
                && TryGetAudioMixerByName(audioModel.AudioMixerName, out var audioMixerGroup))
            {
                return audioMixerGroup;
            }
            
            TryGetAudioMixerByName(DEFAULT_AUDIO_MIXER_NAME, out audioMixerGroup);
            return audioMixerGroup;
        }

        private bool TryGetAudioMixerByName(string audioMixerName, out AudioMixerGroup audioMixerGroup)
        {
            audioMixerGroup = null;
            for (int i = 0; i < _audioMixers.Count; i++)
            {
                var audioMixerGroups = _audioMixers[i].FindMatchingGroups(audioMixerName);
                if (audioMixerGroups.Length > 0)
                {
                    audioMixerGroup = audioMixerGroups[0];
                    return true;
                }
            }

            return false;
        }
    }
}