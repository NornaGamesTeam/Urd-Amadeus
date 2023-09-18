using System;
using System.Collections.Generic;
using Urd.Story;
using Urd.Utils;

namespace Urd.GameManager
{
    public class GameManagerStoryModule : GameManagerModule
    {
        public const int SAVED_DATA_AMOUNT = 3; 
        
        private List<GameStoryModel> _gameStoryModel = new List<GameStoryModel>(SAVED_DATA_AMOUNT);
        private int _currentSlot = -1;
        
        public GameStoryModel CurrentStoryModel => _gameStoryModel[_currentSlot];
        
        public event Action OnStoryStepChanged;
        
        public GameManagerStoryModule()
        {
            LoadSaveData();
        }

        private void LoadSaveData()
        {
            StaticServiceLocator.Get<i>()
        }

        public void NewGame(int slot)
        {
            _gameStoryModel[slot].NewGame();
        }

        public void DeleteSaveData(int slot)
        {
            _gameStoryModel[slot] = new GameStoryModel();
        }

        public void NextStoryStep()
        {
            CurrentStoryModel.NextStoryStep();
            OnStoryStepChanged?.Invoke();
        }
    }
}
