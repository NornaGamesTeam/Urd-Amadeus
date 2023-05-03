using UnityEngine;

namespace Urd.Character
{
    // TODO create from the level manager
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] 
        private CharacterConfig _characterConfig;

        private CharacterMovement _characterMovement;
        
        [field: SerializeField, MyBox.ReadOnly]
        public CharacterModel CharacterModel { get; private set; }

        void Awake()
        {
            Init();
        }

        private void Init()
        {
            CharacterModel = new CharacterModel(_characterConfig);
            _characterMovement = new CharacterMovement(CharacterModel);
        }
    }
}
