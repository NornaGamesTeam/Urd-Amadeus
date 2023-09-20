using System.Data;
using System.Diagnostics.PerformanceData;
using TMPro;
using UnityEngine;
using Urd.Game;

namespace Urd.Popup
{
    public class GameDataSelectionElementView : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI _headerText;
        [SerializeField] 
        private TextMeshProUGUI _percentage;

        private GameDataModel _gameDataModel;
        private GameDataSelectionPopupView _gameDataSelectionPopupView;
        
        
        public void Init(GameDataSelectionPopupView gameDataSelectionPopupView, GameDataModel gameDataModel)
        {
            _gameDataModel = gameDataModel;
            _gameDataSelectionPopupView = gameDataSelectionPopupView;
            
            SetData();
        }

        private void SetData()
        {
            _headerText.text = _gameDataModel.Name;
            _percentage.text = _gameDataModel.TimePlayedInSeconds.ToString();
        }

        public void ClickOnPlay()
        {
            _gameDataSelectionPopupView.ClickOnPlay(_gameDataModel);
        }
        
        public void ClickOnDelete()
        {
            _gameDataSelectionPopupView.ClickOnDeleteSaveData(_gameDataModel);

            SetData();
        }
    }
}