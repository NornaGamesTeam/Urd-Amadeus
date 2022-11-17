using NUnit.Framework;
using System;
using System.Collections;
using System.Drawing.Text;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.TestTools;
using Urd.Error;
using Urd.Services;
using Urd.Services.Network;

namespace Urd.Test
{
    public class TestAssetService
    {
        public const string TEST_LABEL = "Test";

        private IAssetService _assetService;

        private bool _onLoadCallback;
        private bool _loadLabelAnswer;
        private Sprite _loadSprite;

        [SetUp]
        public void SetUp()
        {
            _assetService = new AssetService();
            _assetService.Init();
        }

        [UnityTest]
        public IEnumerator LoadAsset_Sprite_Success()
        {
            yield return new WaitUntil(() => _assetService.IsInitialized);

            _assetService.LoadAsset<Sprite>(TEST_LABEL, OnLoadAssetSprite);

            yield return new WaitUntil(() => _onLoadCallback);

            Assert.That(_loadSprite, Is.Not.Null);
        }

        private void OnLoadAssetSprite(Sprite sprite)
        {
            _onLoadCallback = true;
            _loadSprite = sprite;
        }
    }
}