using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using Urd.Error;
using Urd.UI;
using Urd.Utils;

namespace Urd.Services
{
    public class StartUpServiceView : MonoBehaviour
    {
        private const string LOGO_CONFIG = "StartUp/LogoConfig"; 
        
        [Header("Background")]
        [SerializeField]
        private Transform _backgroundParent;
        
        [Header("LogoImage")]
        [SerializeField]
        private Transform _logoParent;
        
        [Header("ProgressBar")]
        [SerializeField]
        private Transform _progressBarParent;

        private ResourceHelper<LogoConfig> _logoConfig = new (LOGO_CONFIG);
        
        private UIImageController _logoImageController;
        private UIImageController _backgroundImageController;
        private UIProgressBarController _progressBarController;
        
        void Awake()
        {
            var serviceLocator = new ServiceLocator();
            serviceLocator.Register<IStartUpService>(new StartUpService());
        }

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
            LoadProgressBar();
        }


        private void LoadBackground()
        {
            _backgroundImageController = new UIImageController(_logoConfig.FileLoaded.BackgroundModel, _backgroundParent);
        }

        private void LoadLogo()
        {
            _logoImageController = new UIImageController(_logoConfig.FileLoaded.LogoModel, _logoParent);
        }
        private void LoadProgressBar()
        {
            _progressBarController =
                new UIProgressBarController(_logoConfig.FileLoaded.LoadingBarModel, _progressBarParent);
        }
    }
}
