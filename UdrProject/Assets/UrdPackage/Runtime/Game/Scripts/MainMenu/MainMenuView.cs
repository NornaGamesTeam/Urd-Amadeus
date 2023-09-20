using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Urd.Error;
using Urd.Game;
using Urd.Popup;
using Urd.Scene;
using Urd.UI;
using Urd.Utils;

namespace Urd.Services
{
    public class MainMenuView : MonoBehaviour
    {
        private const string LOGO_CONFIG = "LoadingGame/LogoConfig"; 
        
        [Header("Background")]
        [SerializeField]
        private Transform _backgroundParent;
        
        [Header("LogoImage")]
        [SerializeField]
        private Transform _logoParent;
        

        private ResourceHelper<LogoConfig> _logoConfig = new (LOGO_CONFIG);
        
        [Header("Controllers")]
        [SerializeField]
        private UIImageController _logoImageController;
        [SerializeField]
        private UIImageController _backgroundImageController;

        [SerializeField, Header("DEBUG")] 
        private bool _autoBegin;
        
        void Start()
        {
            Init();
        }

        private void Init()
        {
            if (_logoConfig.FileLoaded == null)
            {
                var error = new ErrorModel("[StartUpServiceView] Logo Config not available",
                                           ErrorCode.Error_100_Continue, UnityWebRequest.Result.DataProcessingError);
                Debug.LogWarning(error);
                return;
            }
            
            LoadBackground();
            LoadLogo();

            if (_autoBegin)
            {
                ClickOnContinue();
            }
        }

        private void LoadBackground()
        {
            _backgroundImageController = new UIImageController(_logoConfig.FileLoaded.BackgroundModel, _backgroundParent);
        }

        private void LoadLogo()
        {
            _logoImageController = new UIImageController(_logoConfig.FileLoaded.LogoModel, _logoParent);
        }
        
        public void ClickOnContinue()
        {
            StaticServiceLocator.Get<IGameManagerService>().ContinueGame();
        }

        public void ClickOnPlay()
        {
            var gameDataSelectionModel = new GameDataSelectionPopupModel(); 
            StaticServiceLocator.Get<INavigationService>().Open(gameDataSelectionModel);
        }
        
        public void ClickOnCloseGame()
        {
            StaticServiceLocator.Get<IGameManagerService>().CloseGame();
        }
    }
}
