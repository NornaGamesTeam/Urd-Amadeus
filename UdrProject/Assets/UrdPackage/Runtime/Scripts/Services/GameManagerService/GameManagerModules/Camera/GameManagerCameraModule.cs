using Urd.Game.Camera;
using Urd.Scene;
using Urd.Services;
using Urd.Services.Navigation;
using Urd.Utils;

namespace Urd.GameManager
{
    public class GameManagerCameraModule
    {
        public GameCameraModel GameCameraModel { get; private set; }
        private GameCameraController _gameCameraController;

        private INavigationService _navigationService;

        public GameManagerCameraModule()
        {
            Init();
        }

        public void Init()
        {

            GameCameraModel = new GameCameraModel();
            _gameCameraController = new GameCameraController(GameCameraModel);

            _navigationService = StaticServiceLocator.Get<INavigationService>();
            _navigationService.OnFinishLoadNavigable += OnFinishLoadNavigable;
        }

        private void OnFinishLoadNavigable(INavigable iNavigable)
        {
            // do this better
            if (iNavigable.Id == SceneTypes.Game.ToString())
            {
                _gameCameraController.Init();
            }
        }
    }
}