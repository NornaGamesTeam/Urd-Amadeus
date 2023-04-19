using UnityEngine;

namespace Urd.UI
{
    public class MainMenuConfig : ScriptableObject
    {
        [field: SerializeField]
        public UIImageModel BackgroundModel { get; private set; }
        
        [field: SerializeField]
        public UIImageModel LogoModel { get; private set; }
    }
}