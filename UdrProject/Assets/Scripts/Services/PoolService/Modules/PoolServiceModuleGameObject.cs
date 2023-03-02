using System.Collections.Generic;

using UnityEngine;
using Urd.Utils;

namespace Urd.Services.Pool
{
    public class PoolServiceModuleGameObject
    {
        private Dictionary<string, PoolGameObjectInfo> _elements = new Dictionary<string, PoolGameObjectInfo>();
        private Transform _container;

        public PoolServiceModuleGameObject()
        {
            var poolContainer = GameObject.FindWithTag(CanvasTagNames.PoolCanvas.ToString());
            if (poolContainer != null)
            {
                _container = poolContainer.transform;
            }
            else
            {
                Debug.LogWarning($"Object with tag {CanvasTagNames.PoolCanvas.ToString()}");
                _container = new GameObject(CanvasTagNames.PoolCanvas.ToString()).transform;
                _container.tag = CanvasTagNames.PoolCanvas.ToString();
            }
        }

        public void PreLoadGameObject(GameObject prefab, string namePrefab, int initialAmount, bool replace = false)
        {
            if (!_elements.TryGetValue(namePrefab, out var poolInfo))
            {
                poolInfo = new PoolGameObjectInfo(prefab);
                _elements[namePrefab] = poolInfo;
            }

            if (prefab != poolInfo.Prefab)
            {
                if (!replace)
                {
                    Debug.LogWarning(
                        "[PoolServiceModuleGameObject] Name prefab used for another prefab. Choose another namePrefab to save or set replace = true for replaceOld prefab");
                    return;
                }

                poolInfo = new PoolGameObjectInfo(prefab);
                _elements[namePrefab] = poolInfo;
            }

            if (poolInfo.Elements.Count < initialAmount)
            {
                for (int i = initialAmount - poolInfo.Elements.Count - 1; i >= 0; --i)
                {
                    GameObject go = GameObject.Instantiate<GameObject>(prefab, _container);
                    go.transform.localScale = Vector3.one;
                    go.gameObject.SetActive(false);
                    poolInfo.Elements.Add(go);
                }
            }
        }

        public GameObject GetGameObject(string namePrefab)
        {
            PoolGameObjectInfo poolInfo;
            if (!_elements.TryGetValue(namePrefab, out poolInfo))
            {
                Debug.LogWarning(
                    $"[PoolServiceModuleGameObject] Pool with name {namePrefab} Not exist in the PoolGameObject");
                return null;
            }

            if (poolInfo.Elements.Count <= 0)
            {
                GameObject tempGo = GameObject.Instantiate<GameObject>(poolInfo.Prefab, _container);
                poolInfo.Elements.Add(tempGo);
            }

            GameObject result = poolInfo.Elements[poolInfo.Elements.Count - 1];
            poolInfo.Elements.RemoveAt(poolInfo.Elements.Count - 1);
            return result;
        }

        public void FreeGameObject(string namePrefab, GameObject gameObjectToFree)
        {
            if (_elements.TryGetValue(namePrefab, out var poolInfo))
            {
                poolInfo.Elements.Add(gameObjectToFree);
                gameObjectToFree.transform.SetParent(_container);
                gameObjectToFree.SetActive(false);
            }
            else
            {
                Debug.LogWarning($"[PoolServiceModuleGameObject] Pool with name {namePrefab} Not exist in the PoolGameObject");
            }
        }

        public struct PoolGameObjectInfo
        {
            public GameObject Prefab { get; private set; }
            public List<GameObject> Elements { get; private set; } 

            public PoolGameObjectInfo(GameObject prefab)
            {
                Prefab = prefab;
                Elements = new List<GameObject>();
            }
        } 
    }
}