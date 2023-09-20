using System.Collections.Generic;
using UnityEngine;
using Urd.Game;
using Urd.GameManager;
using Urd.Utils;
using Urd.View.Popup;

namespace Urd.Popup
{
    public class GameDataSelectionPopupView : PopupView<GameDataSelectionPopupModel>
    {
        // TODO move this to scriptableObject
        [SerializeField] 
        private GameDataSelectionElementView _gameDataSelectionElementPrefab;

        [SerializeField] 
        private Transform _elementParent;

        private List<GameDataSelectionElementView> _elementViews = new List<GameDataSelectionElementView>();
        
        private List<GameDataModel> _gameDataModels;
        
        protected override void OnBeginOpen()
        {
            base.OnBeginOpen();

            _gameDataModels = StaticServiceLocator.Get<IGameManagerService>()
                                                  .GetModule<GameManagerGameDataModule>().GameDataModels; 
                
            CreateElements();
        }

        private void CreateElements()
        {
            for (int i = 0; i < _gameDataModels.Count; i++)
            {
                var elementView = GameObject.Instantiate(_gameDataSelectionElementPrefab, _elementParent);
                elementView.Init(this, _gameDataModels[i]);
                _elementViews.Add(elementView);
            }
        }

        public void ClickOnPlay(GameDataModel gameDataModel)
        {
            StaticServiceLocator.Get<IGameManagerService>().LoadGame(gameDataModel);
            Close();
        }

        public void ClickOnDeleteSaveData(GameDataModel gameDataModel)
        {
            StaticServiceLocator.Get<IGameManagerService>().GetModule<GameManagerGameDataModule>().DeleteGame(gameDataModel);
        }
    }
}