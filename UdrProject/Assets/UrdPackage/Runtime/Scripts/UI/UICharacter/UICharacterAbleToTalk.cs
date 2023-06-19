using System;
using UnityEngine;
using Urd.Character;
using Urd.Services.Physics;

namespace Urd.UI
{
    public class UICharacterAbleToTalk : CharacterModelListener
    {
        [SerializeField]
        private CanvasGroup _ableToTalkButton;
        
        private NpcCharacterModel _npcCharacterModel;
        
        public override void Init()
        {
            base.Init();
            _ableToTalkButton.gameObject.SetActive(false);
            _npcCharacterModel = (_characterModel as NpcCharacterModel);
            if (_npcCharacterModel == null)
            {
                return;
            }

            _npcCharacterModel.NPCInteractionsModel.OnAbleToTalkChanged += OnAbleToTalkChanged;
        }

        private void OnEnable()
        {
            if (_npcCharacterModel == null)
            {
                return;
            }
            
            _npcCharacterModel.NPCInteractionsModel.OnAbleToTalkChanged += OnAbleToTalkChanged;
        }

        private void OnDisable()
        {
            _npcCharacterModel.NPCInteractionsModel.OnAbleToTalkChanged -= OnAbleToTalkChanged;
        }

        private void OnAbleToTalkChanged(bool ableToTalk)
        {
            _ableToTalkButton.gameObject.SetActive(ableToTalk);

        }
    }
}