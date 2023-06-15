namespace Urd.Character
{
    public class NPCInteractionsModel
    {
        public string Text => (_characterConfig as NpcCharacterConfig).Text.GetLocalizedString();
        
        private readonly CharacterConfig _characterConfig;

        public NPCInteractionsModel(CharacterConfig characterConfig)
        {
            _characterConfig = characterConfig;
        }

        public void Dispose() { }
    }
}