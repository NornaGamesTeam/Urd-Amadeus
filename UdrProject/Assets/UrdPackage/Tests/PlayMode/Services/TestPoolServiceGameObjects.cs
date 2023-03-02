using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Urd.Pool;
using Urd.Services;

namespace Urd.Test
{
    public class TestPoolServiceGameObjects
    {
        public const string GAMEOBJECT_PATH = "TestAssetObjects/TestGameObject.prefab";
        public const string GAMEOBJECT_ID = "testId";

        private IPoolService _poolService;
        private IAssetService _assetService;
        private GameObject _gameObject;

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            _poolService = new PoolService();
            _poolService.Init();
            
            _assetService = new AssetService();
            _assetService.Init();
            
            yield return new WaitUntil(() => _assetService.IsLoaded);
            _assetService.LoadAsset<GameObject>(GAMEOBJECT_PATH, OnLoadGameObject);
            yield return new WaitUntil(() => _gameObject != null);        
        }

        private void OnLoadGameObject(GameObject newGameObject)
        {
            _gameObject = newGameObject;
        }

        [Test]
        public void PoolService_PreLoadGameObject_Success()
        {
            _poolService.PreLoadGameObject(_gameObject, GAMEOBJECT_ID, 1);

            Assert.That(true, Is.EqualTo(true));
        }
        
        [Test]
        public void PoolService_GetGameObject_Success()
        {
            _poolService.PreLoadGameObject(_gameObject, GAMEOBJECT_ID, 1);
            
            var dummyGameObject = _poolService.GetGameObject(GAMEOBJECT_ID);

            Assert.That(dummyGameObject, Is.Not.Null);
        }
        
        [Test]
        public void PoolService_GetGameObject_SuccessForceCreation()
        {
            _poolService.PreLoadGameObject(_gameObject, GAMEOBJECT_ID, 1);
            
            var dummyGameObject = _poolService.GetGameObject(GAMEOBJECT_ID);
            dummyGameObject = _poolService.GetGameObject(GAMEOBJECT_ID);

            Assert.That(dummyGameObject, Is.Not.Null);
        }
        
        [Test]
        public void PoolService_FreeGameObject_Success()
        {
            _poolService.PreLoadGameObject(_gameObject, GAMEOBJECT_ID, 1);
            var dummyGameObject = _poolService.GetGameObject(GAMEOBJECT_ID);
            
            _poolService.FreeGameObject(GAMEOBJECT_ID, dummyGameObject);

            Assert.That(dummyGameObject, Is.Not.Null);
        }
    }
}