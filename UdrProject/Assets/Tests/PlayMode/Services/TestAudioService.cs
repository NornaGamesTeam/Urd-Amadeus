using System.Collections;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Urd.Audio;
using Urd.Services;

namespace Urd.Test
{
    public class TestAudioService
    {
        private IAudioService _audioService;
        private IAssetService _assetService;
        private IClockService _clockService;

        private bool _audioPausedCallbackCalled;
        private bool _audioStopCallbackCalled;

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            IServiceLocator serviceLocator = new ServiceLocator();

            _assetService = new AssetService();
            serviceLocator.Register<IAssetService>(_assetService);
            
            yield return new WaitUntil(() => _assetService.IsLoaded);

            _audioService = new AudioService();
            serviceLocator.Register<IAudioService>(_audioService);
            yield return new WaitUntil(() => _audioService.IsLoaded);
            
            var coroutineSubstituted = Substitute.For<ICoroutineService>();
            serviceLocator.Register<ICoroutineService>(coroutineSubstituted);
            
            _clockService = new ClockService();
            serviceLocator.Register<IClockService>(_clockService);

            _audioPausedCallbackCalled = false;
            _audioStopCallbackCalled = false;
        }

        [Test]
        public void AudioService_Play_Success()
        {
            var audioModel = _audioService.Play(AudioTypes.TestShort);
            
            Assert.That(audioModel.IsPlaying, Is.EqualTo(true));
        }
        
        [Test]
        public void AudioService_Play_Failed()
        {
            var audioModel = _audioService.Play(AudioTypes.None);
            
            Assert.That(audioModel, Is.Null);
        }
        
        [UnityTest]
        public IEnumerator AudioService_Pause_FailedBecauseFadeIn()
        {
            var audioModel = _audioService.Play(AudioTypes.TestLong);
            
            _audioService.Pause(audioModel, OnPauseCallback);
            _clockService.Update(audioModel.PauseFadeOut-0.1f);
            yield return 0; 
            
            Assert.That(!audioModel.IsInPause && !_audioPausedCallbackCalled, Is.EqualTo(true));
        }

        [UnityTest]
        public IEnumerator AudioService_Pause_Success()
        {
            var audioModel = _audioService.Play(AudioTypes.TestLong);
            _clockService.Update(audioModel.FadeIn+0.1f);
            yield return 0; 
            
            _audioService.Pause(audioModel, OnPauseCallback);
            _clockService.Update(audioModel.PauseFadeOut+0.1f);
            yield return 0; 
            
            Assert.That(audioModel.IsInPause && _audioPausedCallbackCalled, Is.EqualTo(true));
        }
        
        [UnityTest]
        public IEnumerator AudioService_UnPause_Success()
        {
            var audioModel = _audioService.Play(AudioTypes.TestLong);
            _clockService.Update(audioModel.FadeIn+0.1f);
            yield return 0; 
            
            _audioService.Pause(audioModel, OnPauseCallback);
            _clockService.Update(audioModel.PauseFadeOut+0.1f);
            yield return 0;
            
            _audioService.Play(audioModel);
            _clockService.Update(audioModel.PauseFadeIn+0.1f);
            yield return 0; 
            
            Assert.That(audioModel.IsInPause, Is.EqualTo(false));
        }
        
        [UnityTest]
        public IEnumerator AudioService_UnPause_FailedBecauseFadeOut()
        {
            var audioModel = _audioService.Play(AudioTypes.TestLong);
            _clockService.Update(audioModel.FadeIn+0.1f);
            yield return 0; 
            
            _audioService.Pause(audioModel, OnPauseCallback);
            _clockService.Update(audioModel.PauseFadeOut+0.1f);
            yield return 0;
            
            _audioService.Play(audioModel);
            _clockService.Update(audioModel.PauseFadeIn-0.1f);
            yield return 0; 
            
            Assert.That(!audioModel.IsInPause, Is.EqualTo(true));
        }
        
        [UnityTest]
        public IEnumerator AudioService_Stop_Success()
        {
            var audioModel = _audioService.Play(AudioTypes.TestLong);
            _clockService.Update(audioModel.FadeIn+0.1f);
            yield return 0; 
            
            _audioService.Stop(audioModel, OnStopCallback);
            _clockService.Update(audioModel.PauseFadeOut+0.1f);
            yield return 0;
            
            Assert.That(audioModel.IsPlaying, Is.EqualTo(false));
        }
        
        [UnityTest]
        public IEnumerator AudioService_Stop_FailedBecaseuFadeOut()
        {
            var audioModel = _audioService.Play(AudioTypes.TestLong);
            _clockService.Update(audioModel.FadeIn+0.1f);
            yield return 0; 
            
            _audioService.Stop(audioModel, OnStopCallback);
            _clockService.Update(audioModel.PauseFadeOut-0.1f);
            yield return 0;
            
            Assert.That(audioModel.IsPlaying, Is.EqualTo(true));
        }
        
        private void OnPauseCallback()
        {
            _audioPausedCallbackCalled = true;
        }
        
        private void OnStopCallback()
        {
            _audioStopCallbackCalled = true;
        }
    }
}
