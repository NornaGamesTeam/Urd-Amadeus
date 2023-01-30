using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using Urd.Scene;
using Urd.Services;

namespace Urd.Test
{
    public class TestAssetService
    {
        public const string TEST_FOLDER = "TestAssetObjects/";
        public const string TEST_JPG= TEST_FOLDER +"TestJPG.jpeg";
        public const string TEST_PNG = TEST_FOLDER +"TestPNG.png";
        public const string TEST_MP3 = TEST_FOLDER + "TestMP3.mp3";
        public const string TEST_GameObject = TEST_FOLDER + "TestGameObject.prefab";
        public const string TEST_Scene = TEST_FOLDER + "Test.unity";

        private IAssetService _assetService;

        private bool _onLoadCallback;
        private bool _loadLabelAnswer;
        private Sprite _loadSprite;
        private AudioClip _loadAudio;
        private GameObject _loadGameObject;
        private SceneModel _loadSceneInstance;
        private SceneModel _sceneModel;

        [SetUp]
        public void SetUp()
        {
            _assetService = new AssetService();
            _assetService.Init();
            _sceneModel = new SceneModel(SceneTypes.Test);
        }

        [UnityTest]
        public IEnumerator LoadAsset_SpriteJPG_Success()
        {
            yield return new WaitUntil(() => _assetService.IsInitialized);

            _assetService.LoadAsset<Sprite>(TEST_JPG, OnLoadAssetSprite);

            yield return new WaitUntil(() => _onLoadCallback);

            Assert.That(_loadSprite, Is.Not.Null);
        }

        [UnityTest]
        public IEnumerator LoadAsset_SpritePNG_Success()
        {
            yield return new WaitUntil(() => _assetService.IsInitialized);

            _assetService.LoadAsset<Sprite>(TEST_PNG, OnLoadAssetSprite);

            yield return new WaitUntil(() => _onLoadCallback);

            Assert.That(_loadSprite, Is.Not.Null);
        }

        [UnityTest]
        public IEnumerator LoadAsset_AudioMP3_Success()
        {
            yield return new WaitUntil(() => _assetService.IsInitialized);

            _assetService.LoadAsset<AudioClip>(TEST_MP3, OnLoadAssetAudio);

            yield return new WaitUntil(() => _onLoadCallback);

            Assert.That(_loadAudio, Is.Not.Null);
        }

        [UnityTest]
        public IEnumerator LoadAsset_GameObject_Success()
        {
            yield return new WaitUntil(() => _assetService.IsInitialized);

            _assetService.LoadAsset<GameObject>(TEST_GameObject, OnLoadGameObject);

            yield return new WaitUntil(() => _onLoadCallback);

            Assert.That(_loadGameObject, Is.Not.Null);
        }

        [UnityTest]
        public IEnumerator LoadScene_Scene_Success()
        {
            yield return new WaitUntil(() => _assetService.IsInitialized);

            _assetService.LoadScene(_sceneModel, OnSceneCallback);

            yield return new WaitUntil(() => _onLoadCallback);

            Assert.That(_loadSceneInstance, Is.Not.Null);
        }
        private void OnSceneCallback(SceneModel sceneModel)
        {
            _onLoadCallback = true;
            if (sceneModel.HasScene)
            {
                _loadSceneInstance = sceneModel;
            }
        } 

        private void OnLoadGameObject(GameObject gameObject)
        {
            _onLoadCallback = true;
            _loadGameObject= gameObject;
        }
        
        private void OnLoadAssetAudio(AudioClip audio)
        {
            _onLoadCallback = true;
            _loadAudio = audio;
        }

        private void OnLoadAssetSprite(Sprite sprite)
        {
            _onLoadCallback = true;
            _loadSprite = sprite;
        }
    }
}