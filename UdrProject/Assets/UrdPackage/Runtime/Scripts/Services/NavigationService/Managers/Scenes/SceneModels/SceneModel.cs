using UnityEngine.ResourceManagement.ResourceProviders;
using Urd.Services.Navigation;

namespace Urd.Scene
{
    public class SceneModel : Navigable
    {
        private const int EMPTY_BUILD_INDEX = -1;
        public override string Id => SceneType.ToString();
        public SceneTypes SceneType { get; protected set; }
        public SceneInstance SceneInstance { get; protected set; }
        public UnityEngine.SceneManagement.Scene Scene { get; protected set; }
        public bool SetAsActiveScene { get; protected set; }
        public int BuildIndex { get; protected set; } = EMPTY_BUILD_INDEX;
        public bool IsInBuildIndex => BuildIndex >= 0;
        public bool HasScene => SceneInstance.Scene.IsValid() || Scene.IsValid();

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

        public void SetScene(UnityEngine.SceneManagement.Scene scene)
        {
            Scene = scene;
        }
        
        public void CleanScene()
        {
            BuildIndex = EMPTY_BUILD_INDEX;
            Scene = new UnityEngine.SceneManagement.Scene();
            SceneInstance = new SceneInstance();
        }

        public void SetAsActiveSceneAfterOpen(bool setAsActiveScene)
        {
            SetAsActiveScene = setAsActiveScene;
        }
    }
}