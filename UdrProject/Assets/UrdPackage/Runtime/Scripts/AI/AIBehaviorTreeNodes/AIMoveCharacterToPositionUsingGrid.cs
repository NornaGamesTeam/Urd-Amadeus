using MBT;
using UnityEngine;
using UnityEngine.AI;
using Urd.Character;

namespace Urd.AI
{
    [AddComponentMenu("")]
    [MBTNode("Urd/Move Character To Position Using Grid", 501)]
    public class AIMoveCharacterToPositionUsingGrid : Leaf
    {
        [SerializeField]
        private AIReferenceCharacterController characterVariable =
            new AIReferenceCharacterController(VarRefMode.DisableConstant);
        [Space, SerializeField]
        private Vector2Reference destinyVariable = new Vector2Reference(VarRefMode.DisableConstant);

        [Space, SerializeField] private NavMeshAgent _navMeshAgent;

        private float _timestamp;
        private float _movementDuration;
        private Vector2 _initialPosition;
        private Vector2 _movementDirection;

        private ICharacterModel _characterModel;
        private EnemyCharacterInput _enemyCharacterInput;
        
        private NavMeshPath _path;
        private int _pathIndex;

        public override void OnEnter()
        {
            base.OnEnter();

            _characterModel = characterVariable.Value.CharacterModel;
            _enemyCharacterInput = characterVariable.Value.CharacterInput as EnemyCharacterInput;
            
            _timestamp = 0;
            _initialPosition = _characterModel.CharacterMovement.PhysicPosition;

            _pathIndex = 0;
            _navMeshAgent.CalculatePath(destinyVariable.Value, _path);
            _navMeshAgent.speed = _characterModel.CharacterMovement.Speed;
            /*
            
            _movementDirection = (destinyVariable.Value - _initialPosition).normalized; 
            _enemyCharacterInput.SetMovementVector(_movementDirection);
            
            _movementDuration = Vector2.Distance(_initialPosition, destinyVariable.Value) /
                                _characterModel.CharacterMovement.Speed;
                                */
        }

        public override void OnExit()
        {
            _enemyCharacterInput.SetMovementVector(Vector2.zero);
            
            base.OnExit();
        }
        public override NodeResult Execute()
        {
            _timestamp += DeltaTime;
            
            if ( _timestamp >= _movementDuration)
            {
                return NodeResult.success;
            }
            
            _path.
            /*
            Vector2 newPosition =
                Vector2.Lerp(_initialPosition, destinyVariable.Value, _timestamp / _movementDuration);
            _characterModel.CharacterMovement.SetPosition(newPosition);
            */
            
            return NodeResult.running;
        }

    }
}
