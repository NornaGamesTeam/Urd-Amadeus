using UnityEngine;

namespace Urd.Audio
{
    [System.Serializable]
    public class AudioConfigData
    {
        [field: SerializeField]
        public AudioTypes AudioType { get; private set; }
        
        [field: SerializeField]
        public AudioClip Clip { get; private set; }
    }
}