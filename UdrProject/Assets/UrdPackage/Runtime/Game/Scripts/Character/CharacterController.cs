using System;
using UnityEngine;

namespace Urd.Character
{
    // TODO create from the level manager
    public class CharacterController : MonoBehaviour, IDisposable
    {
        [SerializeField] 
        private CharacterConfig _characterConfig;

        private ICharacterInput _input;

        protected CharacterMovementController _movementController;
        protected SkillSetController _skillSetController;
        
        [field: SerializeField, MyBox.ReadOnly]
        public CharacterModel CharacterModel { get; private set; }

        void Awake()
        {
            Init();
        }

        protected virtual void Init()
        {
            CharacterModel = new CharacterModel(_characterConfig);
        }
        
        public void SetInput(ICharacterInput characterInput)
        {
            // TODO move this to other place
            _input = characterInput;
            _movementController = new CharacterMovementController(CharacterModel, _input);
            _skillSetController = new SkillSetController(CharacterModel, _input);
        }
        public void Dispose()
        {
            _movementController?.Dispose();
            _skillSetController?.Dispose();
        }

    }
}
