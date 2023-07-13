using MBT;
using UnityEngine;
using Urd.Character;

namespace Urd.AI
{
    [AddComponentMenu("")]
    [MBTNode("Urd/Move Character To Position", 501)]
    public class AIMoveCharacterToPosition : Leaf
    {
        [SerializeField]
        private AIReferenceCharacterController characterVariable =
            new AIReferenceCharacterController(VarRefMode.DisableConstant);
        [Space, SerializeField]
        private Vector2Reference destinyVariable = new Vector2Reference(VarRefMode.DisableConstant);

        private float _timestamp;
        private float _movementDuration;
        private Vector2 _initialPosition;
        private Vector2 _movementDirection;

        private ICharacterModel _characterModel;
        private EnemyCharacterInput _enemyCharacterInput;
        
        public override void OnEnter()
        {
            base.OnEnter();

            _characterModel = characterVariable.Value.CharacterModel;
            _enemyCharacterInput = characterVariable.Value.CharacterInput as EnemyCharacterInput;
            
            _timestamp = 0;
            _initialPosition = _characterModel.CharacterMovement.PhysicPosition;

            _movementDirection = (destinyVariable.Value - _initialPosition).normalized; 
            _enemyCharacterInput.SetMovementVector(_movementDirection);
            
            _movementDuration = Vector2.Distance(_initialPosition, destinyVariable.Value) /
                                _characterModel.CharacterMovement.Speed;
        }

        public override void OnExit()
        {
            _enemyCharacterInput.SetMovementVector(Vector2.zero);
            
            base.OnExit();
        }
        public override NodeResult Execute()
        {
            _timestamp += DeltaTime;
            
            if (_timestamp >= _movementDuration)
            {
                return NodeResult.success;
            }
            
            /*
            Vector2 newPosition =
                Vector2.Lerp(_initialPosition, destinyVariable.Value, _timestamp / _movementDuration);
            _characterModel.CharacterMovement.SetPosition(newPosition);
            */
            
            return NodeResult.running;
        }

    }
}
