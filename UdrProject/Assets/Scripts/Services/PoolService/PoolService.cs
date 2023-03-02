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

        public void PreLoadClassObject<T>(int initialAmount) where T : class, IPoolable
        {
            Assert.IsTrue(initialAmount > 0, "The initial has to be greater than 0");

            if (IsGameObjectClass(typeof(T)))
            {
                Debug.LogWarning("[PoolService] The type T cannot be GameObject, use PreLoadGameObject instead");
                return;
            }
            _poolServiceModuleClass.PreLoadClass<T>(initialAmount);
        }

        public T GetClassObject<T>() where T : class, IPoolable
        {
            if (IsGameObjectClass(typeof(T)))
            {
                Debug.LogWarning("[PoolService] The type T cannot be GameObject, use GetGameObject instead");
                return null;
            }
            return _poolServiceModuleClass.GetObject<T>();
        }

        public void FreeClassObject<T>(T objectToFree) where T : class, IPoolable
        {
            Assert.IsNotNull(objectToFree, "The Object To free Cannot be null");

            if (IsGameObjectClass(typeof(T)))
            {
                Debug.LogWarning("[PoolService] The type T cannot be GameObject, use FreeGameObject instead");
                return;
            }
            _poolServiceModuleClass.FreeObject<T>(objectToFree);
        }

        public void PreLoadGameObject(GameObject prefab, string prefabId, int initialAmount, bool replace = false)
        {
            Assert.IsTrue(string.IsNullOrEmpty(prefabId), "The PrefabId Cannot be null or empty");
            _poolServiceModuleGameObject.PreLoadGameObject(prefab, prefabId, initialAmount, replace);
        }

        public GameObject GetGameObject(string prefabId)
        {
            Assert.IsTrue(string.IsNullOrEmpty(prefabId), "The PrefabId Cannot be null or empty");
            return _poolServiceModuleGameObject.GetGameObject(prefabId);
        }

        public void FreeGameObject(string prefabId, GameObject gameObjectToFree)
        {
            Assert.IsTrue(string.IsNullOrEmpty(prefabId), "The PrefabId Cannot be null or empty");
            _poolServiceModuleGameObject.FreeGameObject(prefabId, gameObjectToFree);
        }
        
        private bool IsGameObjectClass(Type type)
        {
            return type.IsSubclassOf(typeof(UnityEngine.MonoBehaviour));
        }
    }
}