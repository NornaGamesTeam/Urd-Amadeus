using Urd.Scene;
using Urd.Services;
using Urd.Utils;

namespace Urd.DebugTools
{
    public class DebugCloseScene : DebugAbstract
    {
        public override void OnInputGetDown()
        {
            var sceneModel = new SceneModel(SceneTypes.Info);
            StaticServiceLocator.Get<INavigationService>().Close(sceneModel, null);
        }
    }
}