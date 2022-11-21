using NUnit.Framework;
using System;
using System.Collections;
using System.Drawing.Text;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.TestTools;
using Urd.Error;
using Urd.Services;
using Urd.Services.Network;

namespace Urd.Test
{
    public class TestAssetService
    {
        public const string TEST_FOLDER = "TestAssetObjects/";
        public const string TEST_JPG= TEST_FOLDER +"TestJPG.jpeg";
        public const string TEST_PNG = TEST_FOLDER +"TestPNG.png";
        public const string TEST_MP3 = TEST_FOLDER + "TestMP3.mp3";
        public const string TEST_GameObject = TEST_FOLDER + "TestGameObject.prefab";
        public const string TEST_Scene = TEST_FOLDER + "TestScene.unity";

        private IAssetService _assetService;

        private bool _onLoadCallback;
        private bool _loadLabelAnswer;
        private Sprite _loadSprite;
        private AudioClip _loadAudio;
        private GameObject _loadGameObject;
        private SceneInstance _loadSceneInstance;

        [SetUp]
        public void SetUp()
        {
            _assetService = new AssetService();
            _assetService.Init();
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

            _assetService.LoadScene(TEST_Scene, OnSceneCallback);

            yield return new WaitUntil(() => _onLoadCallback);

            Assert.That(_loadSceneInstance, Is.Not.Null);
        }
        private void OnSceneCallback(SceneInstance sceneInstance)
        {
            _onLoadCallback = true;
            _loadSceneInstance = sceneInstance;
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