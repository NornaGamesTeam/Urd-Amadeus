using System;
using System.Collections.Generic;
using Urd.Game;
using Urd.Story;
using Urd.Utils;

namespace Urd.GameManager
{
    public class GameManagerStoryModule : GameManagerModule
    {
        private List<GameStoryModel> _gameStoryModel;
        

        public GameStoryModel CurrentStoryModel =>
            StaticServiceLocator.Get<IGameManagerService>().GetModule<GameManagerGameDataModule>().CurrentGameDataModel.GameStoryModel;

        public event Action OnStoryStepChanged;

        public override void Init(GameManagerConfig gameManagerConfig)
        {
            base.Init(gameManagerConfig);

            _gameStoryModel = new List<GameStoryModel>(_gameManagerConfig.SaveLoadDataAmount);

            
            LoadSaveData();
        }

        private void LoadSaveData()
        {
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
