using System;
using UnityEngine;
using Urd.Services.Physics;

namespace Urd.Character
{
    // TODO create from the level manager
    public class CharacterController : MonoBehaviour, IDisposable, IHittable, IInteractable
    {
        [SerializeField] 
        private CharacterConfig _characterConfig;

        private ICharacterInput _input;

        protected CharacterMovementController _movementController;
        protected SkillSetController _skillSetController;
        protected CharacterHitPointsController _hitPointsController;
        
        [field: SerializeField, MyBox.ReadOnly]
        public CharacterModel CharacterModel { get; private set; }

        void Awake()
        {
            Init();
        }

        protected virtual void Init()
        {
            CharacterModel = new CharacterModel(_characterConfig);
            
            _hitPointsController = new CharacterHitPointsController(CharacterModel);
        }
        
        public void SetInput(ICharacterInput characterInput)
        {
            // TODO move this to other place
            _input = characterInput;
            _movementController = new CharacterMovementController(CharacterModel, _input, transform.position);
            _skillSetController = new SkillSetController(CharacterModel, _input);
        }
        public void Dispose()
        {
            _movementController?.Dispose();
            _skillSetController?.Dispose();
        }

        public void Hit(float damage, Vector2 hitDirection)
        {
            _hitPointsController.Hit(damage, hitDirection);
        }

        public void Interact()
        {
            Debug.Log("Interact");

        }
    }
}
