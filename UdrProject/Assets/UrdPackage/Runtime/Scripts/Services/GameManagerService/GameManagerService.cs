using System;
using System.Collections.Generic;
using UnityEngine.Device;
using Urd.GameManager;
using Urd.Services;
using Urd.Services.Unity;
using Urd.Utils;

namespace Urd.Game
{
    [Serializable]
    public class GameManagerService : BaseService, IGameManagerService
    {
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

            }
        }

        public T GetModule<T>() where T : class, IGameManagerModule
        {
            return _gameManagerModules.Find(module => module is T) as T;
        }

        public void CloseGame()
        {
            var busService = StaticServiceLocator.Get<IEventBusService>();
            busService.Send(new EventOnUnityClose());
            
            Application.Quit();
        }
    }
}