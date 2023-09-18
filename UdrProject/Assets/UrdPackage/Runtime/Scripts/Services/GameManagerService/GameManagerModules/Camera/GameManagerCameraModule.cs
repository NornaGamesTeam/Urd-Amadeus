using Urd.Game.Camera;
using Urd.Scene;
using Urd.Services;
using Urd.Services.Navigation;
using Urd.Utils;

namespace Urd.GameManager
{
    public class GameManagerCameraModule : GameManagerModule
    {
        public GameCameraModel GameCameraModel { get; private set; }
        private GameCameraController _gameCameraController;

        private ServiceHelper<INavigationService> _navigationService = new ServiceHelper<INavigationService>();

        public GameManagerCameraModule()
        {
            Init();
        }

        public void Init()
        {
            GameCameraModel = new GameCameraModel();
            _gameCameraController = new GameCameraController(GameCameraModel);

            if (_navigationService.HasService)
            {
                SubscribeToNavigation();
            }
            else
            {
                _navigationService.OnInitialize += SubscribeToNavigation;
            }
        }

        private void SubscribeToNavigation()
        {
            _navigationService.Service.OnFinishLoadNavigable += OnFinishLoadNavigable;
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