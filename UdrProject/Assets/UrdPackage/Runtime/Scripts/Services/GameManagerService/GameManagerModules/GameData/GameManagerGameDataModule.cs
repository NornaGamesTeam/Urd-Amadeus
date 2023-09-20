using System;
using System.Collections.Generic;
using Urd.Error;
using Urd.Game;
using Urd.Services;
using Urd.Story;
using Urd.Utils;

namespace Urd.GameManager
{
    [Serializable]
    public class GameManagerGameDataModule : GameManagerModule
    {
        private string LAST_SLOT_PLAYED = "LAST_SLOT_PLAYED";
        private string GAME_DATA_KEY = "GAME_DATA";
        
        private ServiceHelper<ISaveLoadDataService> _saveLoadDataService = new ServiceHelper<ISaveLoadDataService>();
        
        public List<GameDataModel> GameDataModels { get; private set; }
        public GameDataModel CurrentGameDataModel => GameDataModels[_currentSlot];

        public event Action OnLoadGameData;
        
        private int _currentSlot = -1;
        
        public override void Init(GameManagerConfig gameManagerConfig)
        {
            base.Init(gameManagerConfig);

            LoadSaveData();
        }

        private void LoadSaveData()
        {
            _saveLoadDataService.Service.LoadData(LAST_SLOT_PLAYED, 0, OnLastSlotLoaded);
            _saveLoadDataService.Service.LoadData(GAME_DATA_KEY, new List<GameDataModel>(_gameManagerConfig.SaveLoadDataAmount), OnDataLoaded);
        }

        private void OnLastSlotLoaded(ErrorModel errorModel, int value)
        {
            _currentSlot = value;
        }

        private void SaveData()
        {
            _saveLoadDataService.Service.SaveData(GAME_DATA_KEY, GameDataModels, OnDataSaved);
        }
        
        private void OnDataLoaded(ErrorModel errorModel, List<GameDataModel> gameDataModels)
        {
            if (errorModel.IsSuccess)
            {
                GameDataModels = gameDataModels;
                OnLoadGameData?.Invoke();
            }
        }
        
        private void OnDataSaved(ErrorModel errorModel)
        {
            
        }
        
        public void NewGame(int slot)
        {
            GameDataModels[slot].NewGame();
        }
        
        public void DeleteSaveData(int slot)
        {
            GameDataModels[slot] = new GameDataModel();
            SaveData();
        }
    }
}
