using System.Collections.Generic;
using MyBox;
using UnityEngine;
using Urd.Scene;

namespace Urd.Services.Navigation
{
    public class SceneTypesConfig : ScriptableObject
    {
        [SerializeField]
        private List<SceneTypesConfigInfo> _sceneList = new List<SceneTypesConfigInfo>();

        public bool Contains(INavigable navigable)
        {
            var sceneModel = navigable as SceneModel;
            if (sceneModel == null)
            {
                return false;
            }
            
            var sceneType = sceneModel.SceneType;
            return _sceneList.Exists( sceneInfo => sceneInfo.SceneType == sceneType);
        }
    }

    [System.Serializable]
    internal class SceneTypesConfigInfo
    {
        [field: SerializeField]
        public SceneTypes SceneType { get; private set; }
        [field: SerializeField]
        public SceneReference Scene { get; private set; }
    }
}