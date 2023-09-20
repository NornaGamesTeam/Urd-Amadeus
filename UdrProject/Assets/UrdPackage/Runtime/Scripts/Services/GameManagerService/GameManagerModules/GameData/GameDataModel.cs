using Newtonsoft.Json;
using Urd.Story;

namespace Urd.Game
{
    public class GameDataModel
    {
        [JsonProperty]
        public GameStoryModel GameStoryModel { get; private set; }
        
        [JsonProperty]
        public long TimePlayedInSeconds { get; private set; }

        public void NewGame()
        {
            
        }
    }
}
