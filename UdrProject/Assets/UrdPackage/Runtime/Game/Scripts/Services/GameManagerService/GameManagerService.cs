using System;
using Urd.Game.Camera;
using Urd.Scene;
using Urd.Services;
using Urd.Services.Navigation;
using Urd.Utils;

namespace Urd.Game
{
    [Serializable]
    public class GameManagerService : BaseService, IGameManagerService
    {
        public GameCameraModel GameCameraModel { get; private set; }

        private GameCameraController _gameCameraController;

        private INavigationService _navigationService;
        public override void Init()
        {
            base.Init();

            GameCameraModel = new GameCameraModel();
            _gameCameraController = new GameCameraController(GameCameraModel);

            _navigationService = StaticServiceLocator.Get<INavigationService>();
            _navigationService.OnFinishLoadNavigable += OnFinishLoadNavigable;
            
            SetAsLoaded();
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