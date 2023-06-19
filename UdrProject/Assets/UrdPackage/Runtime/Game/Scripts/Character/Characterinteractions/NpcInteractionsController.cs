using System;
using UnityEngine;
using Urd.Game;
using Urd.Utils;

namespace Urd.Character
{
    public class NpcInteractionsController : IDisposable
    {
        private ICharacterModel _characterModel;

        private NPCInteractionsModel _interactionsModel;
        private ServiceHelper<IGameManagerService> _gameManagerService = new ServiceHelper<IGameManagerService>();

        public NpcInteractionsController(ICharacterModel characterModel)
        {
            _characterModel = characterModel;
            _interactionsModel = (_characterModel as NpcCharacterModel).NPCInteractionsModel;
            Init();
        }

        private void Init() { }

        public void Dispose() { }

        public void Interact(Vector3 directionNormalized)
        {
            var text = _interactionsModel.Text;
            _gameManagerService.Service.DialogModule.ShowDialog(text);
            _interactionsModel.Interact(directionNormalized);
        }
    }
}