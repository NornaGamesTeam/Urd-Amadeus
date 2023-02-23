using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Urd.Utils;

namespace Urd.Services
{
    public class StartUpService : BaseService, IStartUpService
    {
        private int _totalElements;
        private List<BaseService> _allServicesToStartUp = new();
        
        private IStartUpService _startUpService;
        public float LoadingFactor => (_allServicesToStartUp.FindAll(service => service.InitBegins).Count 
                                      + _allServicesToStartUp.FindAll(service => service.IsLoaded).Count)
                                      /(_totalElements*2.0f);
        public event Action<float> OnLoadingFactorChanged;
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
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
            StartUpServices();
            ServiceLocatorService.Get<ICoroutineService>().StartCoroutine(CheckRemainingClassesCo());
        }
        
        private void InstantiateServices()
        {
            var servicesTypes = AssemblyHelper.GetClassTypesThatImplement<BaseService>();
            for (int i = servicesTypes.Count - 1; i >= 0; i--)
            {
                if (servicesTypes[i] == typeof(StartUpService))
                {
                    // avoiding to create twice the startup service
                    continue;
                }
                
                var newService = Activator.CreateInstance(servicesTypes[i]) as BaseService;
                _allServicesToStartUp.Add(newService);
                newService.OnFinishLoad += OnFinishLoadService;
            }
            _totalElements = _allServicesToStartUp.Count;
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
            
            SetAsLoaded();
        }
        
        private void OnFinishLoadService()
        {
            for (int i = 0; i < _allServicesToStartUp.Count; i++)
            {
                if (_allServicesToStartUp[i].IsLoaded)
                {
                    _allServicesToStartUp[i].OnFinishLoad -= OnFinishLoadService;
                }
            }   
            
            CallOnLoadingFactorChanged();
        }

        private void CallOnLoadingFactorChanged()
        {
            OnLoadingFactorChanged?.Invoke(LoadingFactor);
        }
    }
}