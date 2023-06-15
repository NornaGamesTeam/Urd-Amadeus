using UnityEngine;
using UnityEngine.Localization;

namespace Urd.Character
{
    public class NpcCharacterConfig : CharacterConfig
    {
        [field: Header("Interactions"), SerializeField] 
        public LocalizedString Text { get; private set; }
    }
}
