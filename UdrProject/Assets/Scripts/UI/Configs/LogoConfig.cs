using UnityEngine;

namespace Urd.UI
{
    public class LogoConfig : ScriptableObject
    {
        [field: SerializeField]
        public UIImageModel BackgroundModel { get; private set; }
        
        [field: SerializeField]
        public UIImageModel LogoModel { get; private set; }
        
        [field: SerializeField]
        public UIProgressBarModel LoadingBarModel { get; private set; }
    }
}