using UnityEngine.ResourceManagement.ResourceProviders;
using Urd.Services.Navigation;

namespace Urd.Scene
{
    public class SceneModel : Navigable
    {
        public override string Id => SceneType.ToString();

        public SceneTypes SceneType { get; protected set; }

        public SceneInstance SceneInstance { get; protected set; }

        public SceneModel(SceneTypes sceneType)
        {
            SceneType = sceneType;
        }

        public void SetSceneInstance(SceneInstance sceneInstance)
        {
            SceneInstance = sceneInstance;
        }
    }
}