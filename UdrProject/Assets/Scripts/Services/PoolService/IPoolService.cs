using UnityEngine;
using Urd.Pool;

namespace Urd.Services
{
    public interface IPoolService : IBaseService
    {
        void PreLoadClass<T>(int initialAmount) where T : class, IPoolable;
        T GetObject<T>() where T : class, IPoolable;
        void FreeObject<T>(T objectToFree) where T : class, IPoolable;
        void PreLoadGameObject(GameObject prefab, string prefabId, int initialAmount, bool replace);
        GameObject GetGameObject(string prefabId);
        void FreeGameObject(string prefabId, GameObject gameObjectToFree);
    }
}