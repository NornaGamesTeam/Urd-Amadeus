using System;
using UnityEngine;

namespace Urd.Character
{
    // TODO create from the level manager
    public class CharacterController : MonoBehaviour, IDisposable
    {
        [SerializeField] 
        private CharacterConfig _characterConfig;

        protected CharacterMovementController _movementController;
        protected SkillSetController SkillSetController;
        
        [field: SerializeField, MyBox.ReadOnly]
        public CharacterModel CharacterModel { get; private set; }

        void Awake()
        {
            Init();
        }

        protected virtual void Init()
        {
            CharacterModel = new CharacterModel(_characterConfig);
            
            _movementController = new CharacterMovementController(CharacterModel);
            SkillSetController = new SkillSetController(CharacterModel);
        }
        public void Dispose()
        {
            _movementController?.Dispose();
            SkillSetController?.Dispose();
        }

    }
}
