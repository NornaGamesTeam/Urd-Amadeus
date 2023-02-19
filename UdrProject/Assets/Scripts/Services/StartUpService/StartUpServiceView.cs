using UnityEngine;
using UnityEngine.UI;

namespace Urd.Services
{
    public class StartUpServiceView : MonoBehaviour
    {
        [Header("LogoImage")] 
        private UIImage _logoImage;
        
        [Header("LogoImage")] 
        private UIProgressBar _logoImage;
        
        void Awake()
        {
            var serviceLocator = new ServiceLocator();
            serviceLocator.Register<IStartUpService>(new StartUpService());
        }
    }
}
