using MyBox;
using UnityEngine;

namespace Urd.Character
{
    public class NpcCharacterModel : CharacterModel
    {
        [field: SerializeField, ReadOnly]
        public NPCInteractionsModel NPCInteractionsModel { get; private set; }

        public NpcCharacterModel(CharacterConfig characterConfig) : base(characterConfig)
        {
            NPCInteractionsModel = new NPCInteractionsModel(characterConfig);
        }
    }
}