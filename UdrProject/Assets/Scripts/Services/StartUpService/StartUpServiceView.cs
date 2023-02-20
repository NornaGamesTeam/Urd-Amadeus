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
        private ServiceHelper<IAssetService> _iAssetService = new ();
        private UIImageModel _logoImageModel;
        
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
        }

        private void LoadBackground()
        {
            _iAssetService.Service.Instantiate(_logoConfig.FileLoaded.BackgroundModel.Addressable, _backgroundParent, OnBackgroundLoaded);
        }

        private void OnBackgroundLoaded(GameObject newBackground)
        {
            newBackground.GetComponent<UIImageView>().SetModel(_logoConfig.FileLoaded.BackgroundModel);
        }

        private void LoadLogo()
        {
            _iAssetService.Service.Instantiate(_logoConfig.FileLoaded.LogoModel.Addressable, _logoParent, OnLogoLoaded);
        }

        private void OnLogoLoaded(GameObject newLogo)
        {
            newLogo.GetComponent<UIImageView>().SetModel(_logoConfig.FileLoaded.LogoModel);
        }
    }
}
