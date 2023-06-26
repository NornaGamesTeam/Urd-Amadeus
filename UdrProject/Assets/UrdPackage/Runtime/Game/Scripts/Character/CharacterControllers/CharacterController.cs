using System;
using UnityEngine;

namespace Urd.Character
{
    // TODO create from the level manager
    public class CharacterController<TCharacterModel> : CharacterControllerNoModel, IDisposable
        where TCharacterModel : class, ICharacterModel
    {
        [SerializeField] private CharacterConfig _characterConfig;

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

        public override void SetInput(ICharacterInput characterInput)
        {
            base.SetInput(characterInput);
           
            _hitPointsController = new CharacterHitPointsController(CharacterModel);
            _movementController = new CharacterMovementController(CharacterModel, CharacterInput, transform.position);
            _skillSetController = new SkillSetController(CharacterModel, CharacterInput);
        }

        public override void Dispose()
        {
            base.Dispose();

            _movementController?.Dispose();
            _skillSetController?.Dispose();
        }
    }
}
