namespace Urd.Character
{
    [System.Serializable]
    public class MainCharacterModel : CharacterModel
    {
        public float InteractionObjectDistance => (_characterConfig as MainCharacterConfig)?.InteractionObjectDistance ?? 0;

        public MainCharacterModel() : base() { }
        public MainCharacterModel(CharacterConfig characterConfig): base(characterConfig) { }
    }
}