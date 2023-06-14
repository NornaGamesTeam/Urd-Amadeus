using System;
using UnityEngine;
using Urd.Services.Physics;

namespace Urd.Character
{
    // TODO create from the level manager
    public class CharacterController<TCharacterModel> : CharacterControllerNoModel, IDisposable
        where TCharacterModel : class, ICharacterModel
    {
        [SerializeField] private CharacterConfig _characterConfig;

        private ICharacterInput _input;

        protected CharacterMovementController _movementController;
        protected SkillSetController _skillSetController;
        protected CharacterHitPointsController _hitPointsController;

        void Awake()
        {
            Init();
        }

        protected override void Init()
        {
            CharacterModel =
                Activator.CreateInstance(typeof(TCharacterModel), args: _characterConfig) as TCharacterModel;

            foreach (var characterModelListener in GetComponentsInChildren<CharacterModelListener>())
            {
                characterModelListener.SetCharacterModel(CharacterModel);
            }
        }

        public void SetInput(ICharacterInput characterInput)
        {
            // TODO move this to other place
            _input = characterInput;
            
            _hitPointsController = new CharacterHitPointsController(CharacterModel);
            _movementController = new CharacterMovementController(CharacterModel, _input, transform.position);
            _skillSetController = new SkillSetController(CharacterModel, _input);
        }

        public override void Dispose()
        {
            base.Dispose();

            _movementController?.Dispose();
            _skillSetController?.Dispose();
        }
    }
}
