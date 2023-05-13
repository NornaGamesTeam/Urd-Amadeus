using UnityEngine;

namespace Urd.Character
{
    // TODO create from the level manager
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] 
        private CharacterConfig _characterConfig;

        protected CharacterMovementController _characterMovementController;
        
        [field: SerializeField, MyBox.ReadOnly]
        public CharacterModel CharacterModel { get; private set; }

        void Awake()
        {
            Init();
        }

        protected virtual void Init()
        {
            CharacterModel = new CharacterModel(_characterConfig);
            
            _characterMovementController = new CharacterMovementController(CharacterModel);
        }
    }
}
