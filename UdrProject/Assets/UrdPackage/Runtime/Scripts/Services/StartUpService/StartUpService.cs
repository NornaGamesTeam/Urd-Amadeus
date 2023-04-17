using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Urd.Scene;
using Urd.Utils;

namespace Urd.Services
{
    public class StartUpService : BaseService, IStartUpService
    {
        private const string STARTUP_CONFIG_FILE_PATH = "Services/StartUpConfig";
        
        private int _totalElements;
        private List<IBaseService> _allServicesToStartUp = new();
        
        private IStartUpService _startUpService;
        public float LoadingFactor => (_allServicesToStartUp.FindAll(service => service.InitBegins).Count 
                                      + _allServicesToStartUp.FindAll(service => service.IsLoaded).Count)
                                      /(_totalElements*2.0f);
        public event Action<float> OnLoadingFactorChanged;

        private ServiceHelper<INavigationService> _navigationService = new ServiceHelper<INavigationService>();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        private static void OnLoadGame()
        {
            InitStartUpService();
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
            ServiceLocatorService.Get<ICoroutineService>().StartCoroutine(CheckRemainingClassesCo());
            _navigationService.OnInitialize += OnInitializeNavigationService;
        }

        private void InitICoroutine()
        {
            ICoroutineService coroutineService = _allServicesToStartUp.Find(service => service.GetType().ToString().Contains("oroutine")) as ICoroutineService;
            ServiceLocatorService.Register<ICoroutineService>(coroutineService);
        }

        private void InstantiateServices()
        {
            var startUpServiceConfig = Resources.Load<StartUpServiceConfig>(STARTUP_CONFIG_FILE_PATH);
            _allServicesToStartUp = startUpServiceConfig.BaseServices;
            _totalElements = startUpServiceConfig.BaseServices.Count;
        }

        private void StartUpServices()
        {
            for (int i = _allServicesToStartUp.Count - 1; i >= 0; i--)
            {
                if (!_allServicesToStartUp[i].InitBegins && _allServicesToStartUp[i].CanBeInitialized())
                {
                    ServiceLocatorService.Register(_allServicesToStartUp[i], _allServicesToStartUp[i].GetMainInterface());
                    CallOnLoadingFactorChanged();
                }
            }
        }
        
        private IEnumerator CheckRemainingClassesCo()
        {
            while(_allServicesToStartUp.FindAll(service => !service.IsLoaded).Count > 0)
            {
                yield return 0;
                StartUpServices();
            }

            CallOnLoadingFactorChanged();
            SetAsLoaded();
        }

        private void CallOnLoadingFactorChanged()
        {
            OnLoadingFactorChanged?.Invoke(LoadingFactor);
        }
        
        private void OnInitializeNavigationService()
        {
            _navigationService.Service.Open(new SceneModel(SceneTypes.LoadingGame));
        }
    }
}