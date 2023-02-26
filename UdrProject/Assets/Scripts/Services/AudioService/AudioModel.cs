
namespace Urd.Audio
{
    public class AudioModel
    {
        public AudioTypes AudioTypes => _audioConfigData.AudioType; 

        private AudioConfigData _audioConfigData;
        public AudioModel(AudioConfigData audioConfigData)
        {
            _audioConfigData = audioConfigData;
        }
    }
}