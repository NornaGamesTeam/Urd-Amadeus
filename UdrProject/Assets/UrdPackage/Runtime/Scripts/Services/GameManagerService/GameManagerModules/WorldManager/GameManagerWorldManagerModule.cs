using UnityEngine;
using Urd.Error;
using Urd.Game;
using Urd.Scene;
using Urd.Services;
using Urd.Services.Navigation;
using Urd.Utils;

namespace Urd.GameManager
{
    public class GameManagerWorldManagerModule : GameManagerModule
    {
        private const string WORLD_MANAGER_CONFIG = "World/WorldManageConfig";

        private ResourceHelper<WorldManagerConfig> _worldManagerConfig = 
            new (WORLD_MANAGER_CONFIG, false);

        public override void Init(GameManagerConfig gameManagerConfig)
        {
            base.Init(gameManagerConfig);
        }

        public override void LoadGame(GameDataModel gameDataModel)
        {
            base.LoadGame(gameDataModel);

            StaticServiceLocator.Get<INavigationService>().OnFinishLoadNavigable += OnNavigableLoaded;
        }

        private void OnNavigableLoaded(INavigable iNavigable)
        {
            
            if ((iNavigable as SceneModel)?.SceneType == SceneTypes.Game)
            {
                StaticServiceLocator.Get<INavigationService>().OnFinishLoadNavigable -= OnNavigableLoaded;
                
                LoadMap();
            }
        }

        private void LoadMap()
        {
            
            if (!_worldManagerConfig.FileLoaded.TryGetArea(_gameDataModel.WorldAreaType, out var gameWorldAreaViewPrefab))
            {
                string errorMessage = $"[GameManagerWorldManagerModule] View with WorldAreaType {_gameDataModel.WorldAreaType} No found";
                var errorModel = new ErrorModel(errorMessage, ErrorCode.Error_404_Not_Found);
                return;
            }
            
            var gameWorldAreaView = GameObject.Instantiate(gameWorldAreaViewPrefab);
            gameWorldAreaView.SetModel(_gameDataModel.CurrentGameWorldModel);
        }
    }
}