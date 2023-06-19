using System;
using UnityEngine;

namespace Urd.Character
{
    public class NPCInteractionsModel
    {
        public string Text => (_characterConfig as NpcCharacterConfig).Text.GetLocalizedString();
        public bool AbleToTalk { get; set; }

        public event Action<Vector2> OnInteract;
        public event Action<bool> OnAbleToTalkChanged;
        
        private readonly CharacterConfig _characterConfig;

        public NPCInteractionsModel(CharacterConfig characterConfig)
        {
            _characterConfig = characterConfig;
        }

        public void Dispose() { }

        public void SetAsAbleToTalk(bool showInteractButton)
        {
            if (AbleToTalk == showInteractButton)
                return;
            
            AbleToTalk = showInteractButton;
            OnAbleToTalkChanged?.Invoke(AbleToTalk);
        }

        public void Interact(Vector3 directionNormalized)
        {
            OnInteract?.Invoke(directionNormalized);
        }
    }
}