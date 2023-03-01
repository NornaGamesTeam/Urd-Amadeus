using System;
using UnityEngine;
using UnityEngine.Assertions;
using Urd.Pool;
using Urd.Services.Pool;

namespace Urd.Services
{
    public class PoolService : BaseService, IPoolService
    {
        private PoolServiceModuleClass _poolServiceModuleClass;
        private PoolServiceModuleGameObject _poolServiceModuleGameObject;

        public override void Init()
        {
            base.Init();
            
            _poolServiceModuleClass = new PoolServiceModuleClass();
            _poolServiceModuleGameObject = new PoolServiceModuleGameObject();
            
            SetAsLoaded();
        }

        public void PreLoadClass<T>(int initialAmount) where T : class, IPoolable
        {
            Assert.IsTrue(initialAmount > 0, "The initial has to be greater than 0");

            if (IsGameObjectClass(typeof(T)))
            {
                Debug.LogWarning("[PoolService] The type T cannot be GameObject, use PreLoadGameObject instead");
                return;
            }
            _poolServiceModuleClass.PreLoadClass<T>(initialAmount);
        }

        public T GetObject<T>() where T : class, IPoolable
        {
            if (IsGameObjectClass(typeof(T)))
            {
                Debug.LogWarning("[PoolService] The type T cannot be GameObject, use GetGameObject instead");
                return null;
            }
            return _poolServiceModuleClass.GetObject<T>();
        }

        public void FreeObject<T>(T objectToFree) where T : class, IPoolable
        {
            Assert.IsNull(objectToFree, "The PrefabId Cannot be null or empty");

            if (IsGameObjectClass(typeof(T)))
            {
                Debug.LogWarning("[PoolService] The type T cannot be GameObject, use FreeGameObject instead");
                return;
            }
            _poolServiceModuleClass.FreeObject<T>(objectToFree);
        }

        public void PreLoadGameObject(GameObject prefab, string prefabId, int initialAmount, bool replace = false)
        {
            Assert.IsFalse(string.IsNullOrEmpty(prefabId), "The PrefabId Cannot be null or empty");
            _poolServiceModuleGameObject.PreLoadGameObject(prefab, prefabId, initialAmount, replace);
        }

        public GameObject GetGameObject(string prefabId)
        {
            Assert.IsFalse(string.IsNullOrEmpty(prefabId), "The PrefabId Cannot be null or empty");
            return _poolServiceModuleGameObject.GetGameObject(prefabId);
        }

        public void FreeGameObject(string prefabId, GameObject gameObjectToFree)
        {
            Assert.IsFalse(string.IsNullOrEmpty(prefabId), "The PrefabId Cannot be null or empty");
            _poolServiceModuleGameObject.FreeGameObject(prefabId, gameObjectToFree);
        }
        
        private bool IsGameObjectClass(Type type)
        {
            return type.IsSubclassOf(typeof(UnityEngine.MonoBehaviour));
        }
    }
}