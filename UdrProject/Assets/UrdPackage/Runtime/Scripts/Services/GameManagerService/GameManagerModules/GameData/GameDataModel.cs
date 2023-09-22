using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using Urd.Story;
using Urd.World;

namespace Urd.Game
{
    public class GameDataModel
    {
        [JsonProperty]
        public string Name { get; private set; }
        
        [JsonProperty]
        public WorldAreaTypes WorldAreaType { get; private set; }
        
        [JsonProperty]
        public GameStoryModel GameStoryModel { get; private set; }
        
        [JsonProperty] 
        public List<GameWorldModel> GameWorldModels { get; private set; }
        
        [JsonProperty]
        public long TimePlayedInSeconds { get; private set; }

        public bool HasData => TimePlayedInSeconds > 0;

        public GameWorldModel CurrentGameWorldModel => GetGameWorldModel(WorldAreaType);
        
        public GameDataModel()
        {
            Init();
        }

        private void Init()
        {
            Name = "NO DATA";
            TimePlayedInSeconds = 0;
            GameStoryModel = new GameStoryModel();
            GameWorldModels = new List<GameWorldModel>();
            GameWorldModels.Add(new GameWorldModel());
        }

        public void NewGame()
        {
            
        }

        public void LoadGame()
        {
            TimePlayedInSeconds = Random.Range(10,100000);
            Name = Random.value.ToString();
        }

        public GameWorldModel GetGameWorldModel(WorldAreaTypes worldAreaType) =>
            GameWorldModels.Find(gameWorldModel => gameWorldModel.WorldAreaType == worldAreaType); 

        public void ClearData()
        {
            Init();
        }

        
    }
}
