using UnityEngine;

namespace Urd.UI
{
    public class LogoConfig : ScriptableObject
    {
        [field: SerializeField]
        public Sprite LogoImage { get; private set; }
    }
}