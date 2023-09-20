using System;
using System.Collections.Generic;
using UnityEngine.Device;
using Urd.GameManager;
using Urd.Scene;
using Urd.Services;
using Urd.Services.Unity;
using Urd.Utils;

namespace Urd.Game
{
    [Serializable]
    public class GameManagerService : BaseService, IGameManagerService
    {
        private const string GAME_MANAGER_CONFIG_PATH = "GameManagerConfig";

        public GameManagerConfig GameManagerConfig => _gameManagerConfigResourceHelper.FileLoaded;

        private ResourceHelper<GameManagerConfig> _gameManagerConfigResourceHelper =
            new ResourceHelper<GameManagerConfig>(GAME_MANAGER_CONFIG_PATH);
        
        private List<IGameManagerModule> _gameManagerModules = new List<IGameManagerModule>();

        public override void Init()
        {
            base.Init();

            InitModules();
            SetAsLoaded();
        }

        private void InitModules()
        {
            var classGameModules = AssemblyHelper.GetClassTypesThatImplement<IGameManagerModule>();

            for (int i = 0; i < classGameModules.Count; i++)
            {
                var gameManagerModule = Activator.CreateInstance(classGameModules[i]) as IGameManagerModule;
                _gameManagerModules.Add(gameManagerModule);
                gameManagerModule.Init(GameManagerConfig);
            }
        }

        public T GetModule<T>() where T : class, IGameManagerModule
        {
            return _gameManagerModules.Find(module => module is T) as T;
        }

        public void NewGame()
        {
            LoadGameScene();
        }

        public void ContinueGame()
        {
            
        }

        private void LoadGameScene()
        {
            var sceneModel = new SceneModel(SceneTypes.Game);
            sceneModel.SetAsActiveSceneAfterOpen(true);
            StaticServiceLocator.Get<INavigationService>().Open(sceneModel);
            sceneModel = new SceneModel(SceneTypes.MainMenu);
            StaticServiceLocator.Get<INavigationService>().Close(sceneModel);
        }

        public void CloseGame()
        {
            var busService = StaticServiceLocator.Get<IEventBusService>();
            busService.Send(new EventOnUnityClose());
            
            Application.Quit();
        }
    }
}