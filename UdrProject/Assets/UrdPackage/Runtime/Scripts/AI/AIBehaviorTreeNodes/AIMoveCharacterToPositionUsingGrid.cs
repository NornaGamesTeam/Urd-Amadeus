using DG.DemiEditor;
using MBT;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Urd.Character;

namespace Urd.AI
{
    [AddComponentMenu("")]
    [MBTNode("Urd/Move Character To Position Using Grid", 501)]
    public class AIMoveCharacterToPositionUsingGrid : Leaf
    {
        [Header("Editor"), SerializeField]
        private bool _drawGizmos;
        
        [SerializeField] private AIReferenceCharacterController characterVariable =
            new AIReferenceCharacterController(VarRefMode.DisableConstant);

        [Space, SerializeField]
        private Vector2Reference _destinyVariable = new Vector2Reference(VarRefMode.DisableConstant);

        [Space, SerializeField] private NavMeshAgent _navMeshAgent;

        private float _timestamp;
        private float _movementDuration;
        private Vector2 _lastPosition;
        private Vector2 _nextPosition;

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
            _lastPosition = _characterModel.CharacterMovement.Position;
            _nextPosition = _characterModel.CharacterMovement.Position;

            _pathIndex = 0;
            _navMeshAgent.transform.position = _characterModel.CharacterMovement.Position;
            _path = new NavMeshPath();
            _navMeshAgent.CalculatePath(_destinyVariable.Value, _path);
            _navMeshAgent.speed = _characterModel.CharacterMovement.Speed;

            CalculateNextPosition();
        }

        private bool CalculateNextPosition()
        {
            _pathIndex++;

            if (_pathIndex >= _path.corners.Length)
            {
                return false;
            }
            _lastPosition = _nextPosition;
            _nextPosition = _path.corners[_pathIndex];
            _movementDirection = (_nextPosition - _lastPosition).normalized;
            _enemyCharacterInput.SetMovementVector(_movementDirection);
            _movementDuration =
                Vector2.Distance(_lastPosition, _nextPosition) * _characterModel.CharacterMovement.Speed;
            return true;
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
                if(!CalculateNextPosition())
                {
                    _characterModel.CharacterMovement.TrySetPhysicPosition(_nextPosition);
                    Debug.Log($"Next Pos: {_nextPosition}");
                    return NodeResult.success;
                }
                _timestamp = 0;
            }

            Vector2 newPosition =
                Vector2.Lerp(_lastPosition, _nextPosition, _timestamp / _movementDuration);
            _characterModel.CharacterMovement.TrySetPhysicPosition(newPosition);

            return NodeResult.running;
        }

        private void OnDrawGizmos()
        {
            if (!_drawGizmos)
            {
                return;
            }

            for (int i = 1; i < _path.corners.Length; i++)
            {
                if (i < _pathIndex)
                {
                    Gizmos.color = Color.white.SetAlpha(0.5f);;
                }
                else if(i == _pathIndex)
                {
                    Gizmos.color = Color.yellow.SetAlpha(0.5f);;
                }
                else
                {
                    Gizmos.color = Color.red.SetAlpha(0.5f);;
                }
                    
                Gizmos.DrawLine(_path.corners[i-1], _path.corners[i]);
            }
        }
    }
}
