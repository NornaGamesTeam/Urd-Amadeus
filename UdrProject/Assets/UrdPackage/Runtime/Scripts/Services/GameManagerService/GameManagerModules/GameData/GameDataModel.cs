using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.AddressableAssets.Initialization;
using Urd.Story;

namespace Urd.Game
{
    public class GameDataModel
    {
        [JsonProperty]
        public string Name { get; private set; }
        
        [JsonProperty]
        public GameStoryModel GameStoryModel { get; private set; }
        
        [JsonProperty]
        public long TimePlayedInSeconds { get; private set; }

        public bool HasData => TimePlayedInSeconds > 0;
        
        public GameDataModel()
        {
            Init();
        }

        private void Init()
        {
            Name = "NO DATA";
            TimePlayedInSeconds = 0;
            GameStoryModel = new GameStoryModel();
        }

        public void NewGame()
        {
            
        }

        public void LoadGame()
        {
            TimePlayedInSeconds = Random.Range(10,100000);
            Name = Random.value.ToString();
        }

        public void ClearData()
        {
            Init();
        }

        
    }
}
