using System;
using UnityEngine;
using Urd.Error;
using Urd.Game;
using Urd.Scene;
using Urd.Services;
using Urd.Services.Navigation;
using Urd.Utils;
using Urd.World;

namespace Urd.GameManager
{
    public class GameManagerWorldManagerModule : GameManagerModule
    {
        private const string WORLD_MANAGER_CONFIG = "World/WorldManageConfig";

        private ResourceHelper<WorldManagerConfig> _worldManagerConfig = 
            new (WORLD_MANAGER_CONFIG, false);

        public WorldAreaView _currentWorldArea;
        
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
                
                LoadMap(_gameDataModel.WorldAreaType);
            }
        }

        public void LoadMap(WorldAreaTypes worldAreaTypes)
        {
            ShowMapTransition(worldAreaTypes);
        }

        private void ShowMapTransition(WorldAreaTypes worldAreaTypes)
        {
            // TODO show map transition
            OnFinishMapTransition(worldAreaTypes);
        }

        private void OnFinishMapTransition(WorldAreaTypes worldAreaTypes)
        {
            UnLoadCurrentMap();
            LoadNewMap(worldAreaTypes);
        }
        
        private void UnLoadCurrentMap()
        {
            if (_currentWorldArea != null)
            {
                GameObject.Destroy(_currentWorldArea.gameObject);
            }
        }

        private void LoadNewMap(WorldAreaTypes worldAreaType)
        {
            if (!_worldManagerConfig.FileLoaded.TryGetArea(_gameDataModel.WorldAreaType, out var gameWorldAreaViewPrefab))
            {
                string errorMessage = $"[GameManagerWorldManagerModule] View with WorldAreaType {_gameDataModel.WorldAreaType} No found";
                var errorModel = new ErrorModel(errorMessage, ErrorCode.Error_404_Not_Found);
                return;
            }
            
            _gameDataModel.SetWorldAreaType(worldAreaType);
            _currentWorldArea = GameObject.Instantiate(gameWorldAreaViewPrefab);
            _currentWorldArea.SetModel(_gameDataModel.CurrentGameWorldModel);
        }
    }
}