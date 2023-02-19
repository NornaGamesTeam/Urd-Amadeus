using UnityEngine;
using UnityEngine.UI;

namespace Urd.Services
{
    public class StartUpServiceView : MonoBehaviour
    {
        [Header("LogoImage")] 
        private UIImage _logoImage;
        
        [Header("ProgressBar")] 
        private UIProgressBar _progressBar;
        
        void Awake()
        {
            var serviceLocator = new ServiceLocator();
            serviceLocator.Register<IStartUpService>(new StartUpService());
        }
    }
}
