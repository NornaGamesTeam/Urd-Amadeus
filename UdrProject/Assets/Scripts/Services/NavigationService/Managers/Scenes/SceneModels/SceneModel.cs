using UnityEngine.ResourceManagement.ResourceProviders;
using Urd.Services.Navigation;

namespace Urd.Scene
{
    public class SceneModel : Navigable
    {
        public override string Id => SceneType.ToString();

        public SceneTypes SceneType { get; protected set; }

        public SceneInstance SceneInstance { get; protected set; }
        public int BuildIndex { get; protected set; } = -1;
        public bool IsInBuildIndex => BuildIndex >= 0; 

        public SceneModel(SceneTypes sceneType)
        {
            SceneType = sceneType;
        }

        public void SetSceneInstance(SceneInstance sceneInstance)
        {
            SceneInstance = sceneInstance;
        }

        public void SetBuildIndex(int buildIndex)
        {
            BuildIndex = buildIndex;
        }
    }
}