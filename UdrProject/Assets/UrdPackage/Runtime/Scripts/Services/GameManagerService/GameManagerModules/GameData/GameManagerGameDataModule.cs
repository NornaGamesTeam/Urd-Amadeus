using System;
using System.Collections.Generic;
using System.Dynamic;
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

        public List<GameDataModel> GameDataModels { get; private set; } = new List<GameDataModel>();
        public GameDataModel CurrentGameDataModel => GameDataModels[_currentSlot];
        public bool HasData => _currentSlot >= 0;

        public event Action OnLoadGameData;
        
        private int _currentSlot = -1;

        public event Action OnCurrentSlotChanged; 
        
        public override void Init(GameManagerConfig gameManagerConfig)
        {
            base.Init(gameManagerConfig);

            LoadSaveData();
        }

        private void LoadSaveData()
        {
            GameDataModels = new List<GameDataModel>();
            for (int i = 0; i < _gameManagerConfig.SaveLoadDataAmount; i++)
            {
                GameDataModels.Add(new GameDataModel());
            }
            
            _saveLoadDataService.Service.LoadData(LAST_SLOT_PLAYED, -1, OnCurrentSlotLoaded);
            _saveLoadDataService.Service.LoadData(GAME_DATA_KEY, new List<GameDataModel>(_gameManagerConfig.SaveLoadDataAmount), OnDataLoaded);
        }

        private void OnCurrentSlotLoaded(ErrorModel errorModel, int value)
        {
            _currentSlot = value;
            OnCurrentSlotChanged?.Invoke();
        }

        private void SaveData()
        {
            _saveLoadDataService.Service.SaveData(GAME_DATA_KEY, GameDataModels, OnDataSaved);
            _saveLoadDataService.Service.SaveData(LAST_SLOT_PLAYED, _currentSlot, OnCurrentSloSaved);

        }

        private void OnCurrentSloSaved(ErrorModel errorModel)
        {
            
        }

        private void OnDataLoaded(ErrorModel errorModel, List<GameDataModel> gameDataModels)
        {
            if (errorModel.IsSuccess)
            {
                GameDataModels = gameDataModels;
            }

            OnLoadGameData?.Invoke();
        }

        private void OnDataSaved(ErrorModel errorModel)
        {
            
        }
        
        public void LoadGame(GameDataModel gameDataModel)
        {
            var index = GetIndex(gameDataModel);

            GameDataModels[index].LoadGame();
            _currentSlot = index;
            OnCurrentSlotChanged?.Invoke();
            
            SaveData();
        }

        public void DeleteGame(GameDataModel gameDataModel)
        {
            var index = GetIndex(gameDataModel);
            GameDataModels[index].ClearData();
            if (_currentSlot == index)
            {
                _currentSlot = GetNewCurrentSlot();
                OnCurrentSlotChanged?.Invoke();
            }
            SaveData();
        }

        private int GetNewCurrentSlot()
        {
            for (int i = 0; i < GameDataModels.Count; i++)
            {
                if (GameDataModels[i].HasData)
                {
                    return i;
                }
            }

            return -1;
        }

        private int GetIndex(GameDataModel gameDataModel)
        {
            return GameDataModels.FindIndex(
                gameData => 
                    gameData.Name == gameDataModel.Name
                    && gameData.TimePlayedInSeconds == gameDataModel.TimePlayedInSeconds);
        }
    }
}
