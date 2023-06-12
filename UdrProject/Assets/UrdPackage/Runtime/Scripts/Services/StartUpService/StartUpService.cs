using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Urd.Scene;
using Urd.Utils;

namespace Urd.Services
{
    public class StartUpService : BaseService, IStartUpService
    {
        private const string STARTUP_CONFIG_FILE_PATH = "Services/StartUpConfig";

        private int _totalElements;
        private List<IBaseService> _allServicesToStartUp;

        private IStartUpService _startUpService;

        public float LoadingFactor => (_allServicesToStartUp.FindAll(service => service.InitBegins).Count
                                       + _allServicesToStartUp.FindAll(service => service.IsLoaded).Count)
                                      / (_totalElements * 2.0f);

        public event Action<float> OnLoadingFactorChanged;

        private readonly ServiceHelper<INavigationService> _navigationService = new ServiceHelper<INavigationService>();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void OnLoadGame()
        {
            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                SceneManager.sceneLoaded += OnSceneLoaded;
                return;
            }

            InitStartUpService();
        }

        private static void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode sceneMode)
        {
            if (scene.buildIndex == 0)
            {
                SceneManager.sceneLoaded -= OnSceneLoaded;
                InitStartUpService();
            }
        }

        private static void InitStartUpService()
        {
            var startUpService = AssemblyHelper.GetClassTypesThatImplement<IStartUpService>();
            var startUpInstance = Activator.CreateInstance(startUpService[0]) as StartUpService;

            var serviceLocator = new ServiceLocator();
            serviceLocator.Register<IStartUpService>(startUpInstance);
        }

        public override void Init()
        {
            base.Init();

            InstantiateServices();
            InitICoroutine();
            _navigationService.OnInitialize += OnInitializeNavigationService;
            ServiceLocatorService.Get<ICoroutineService>().StartCoroutine(CheckRemainingClassesCo());
        }

        private void InitICoroutine()
        {
            ICoroutineService coroutineService =
                _allServicesToStartUp.Find(service => service.GetType().ToString().Contains("oroutine")) as
                    ICoroutineService;
            ServiceLocatorService.Register<ICoroutineService>(coroutineService);
        }

        private void InstantiateServices()
        {
            var startUpServiceConfig = Resources.Load<StartUpServiceConfig>(STARTUP_CONFIG_FILE_PATH);
            _allServicesToStartUp = startUpServiceConfig.ListOfServices;
            _totalElements = startUpServiceConfig.ListOfServices.Count;
        }

        private void StartUpServices()
        {
            for (int i = _allServicesToStartUp.Count - 1; i >= 0; i--)
            {
                if (!_allServicesToStartUp[i].InitBegins && _allServicesToStartUp[i].CanBeInitialized())
                {
                    ServiceLocatorService.Register(_allServicesToStartUp[i],
                                                   _allServicesToStartUp[i].GetMainInterface());
                    CallOnLoadingFactorChanged();
                }
            }
        }

        private IEnumerator CheckRemainingClassesCo()
        {
            while (_allServicesToStartUp.FindAll(service => !service.IsLoaded).Count > 0)
            {
                yield return 0;
                StartUpServices();
            }

            yield return 0;
            OnFinishLoadServices();
        }

        private void OnFinishLoadServices()
        {
            CallOnLoadingFactorChanged();
            SetAsLoaded();

            _navigationService.Service.Close(new SceneModel(SceneTypes.LoadingGame), OnCloseLoadingScene);
        }

        private void OnCloseLoadingScene(bool obj)
        {
            var sceneModel = new SceneModel(SceneTypes.MainMenu);
            sceneModel.SetAsActiveSceneAfterOpen(true);

            _navigationService.Service.Open(sceneModel);
        }

        private void CallOnLoadingFactorChanged()
        {
            OnLoadingFactorChanged?.Invoke(LoadingFactor);
        }

        private void OnInitializeNavigationService()
        {
            var sceneModel = new SceneModel(SceneTypes.LoadingGame);
            sceneModel.SetAsActiveSceneAfterOpen(true);
            _navigationService.Service.Open(sceneModel);
        }
    }
}