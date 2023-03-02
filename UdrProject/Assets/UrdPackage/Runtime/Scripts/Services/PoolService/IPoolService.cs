using UnityEngine;
using Urd.Pool;

namespace Urd.Services
{
    public interface IPoolService : IBaseService
    {
        void PreLoadClassObject<T>(int initialAmount) where T : class, IPoolable;
        T GetClassObject<T>() where T : class, IPoolable;
        void FreeClassObject<T>(T objectToFree) where T : class, IPoolable;
        void PreLoadGameObject(GameObject prefab, string prefabId, int initialAmount, bool replace = true);
        GameObject GetGameObject(string prefabId);
        void FreeGameObject(string prefabId, GameObject gameObjectToFree);
    }
}