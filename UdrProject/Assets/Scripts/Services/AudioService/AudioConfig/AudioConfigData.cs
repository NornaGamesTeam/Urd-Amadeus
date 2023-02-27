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
        [field: SerializeField, Range(0f,1f)]
        public float Volume { get; private set; }
        [field: SerializeField]
        public bool IsLoopable { get; private set; }
        [field: SerializeField]
        public string AudioMixerName { get; private set; }
        [field: SerializeField, Header("Fades")]
        public float FadeIn { get; private set; }
        [field: SerializeField]
        public float PauseFadeIn { get; private set; }
        [field: SerializeField]
        public float PauseFadeOut { get; private set; }
        [field: SerializeField]
        public float FadeOut { get; private set; }
    }
}