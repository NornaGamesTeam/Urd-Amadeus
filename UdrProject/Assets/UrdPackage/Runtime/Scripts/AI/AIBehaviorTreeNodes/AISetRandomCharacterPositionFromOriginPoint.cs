using MBT;
using UnityEngine;

namespace Urd.AI
{
    [AddComponentMenu("")]
    [MBTNode("Urd/Set Random Character Position From Origin Point", 501)]
    public class AISetRandomCharacterPositionFromOriginPoint : Leaf
    {
        [SerializeField]
        private AIReferenceCharacterController characterVariable =
            new AIReferenceCharacterController(VarRefMode.DisableConstant);
        
        [SerializeField]
        private Vector2 _boxSize;
        [Space, SerializeField]
        private Vector2Reference destinyVariable = new Vector2Reference(VarRefMode.DisableConstant);

        private Vector2 _originalPoint;
        private void Start()
        {
            _originalPoint = characterVariable.Value.CharacterModel.CharacterMovement.Position;
        }

        public override NodeResult Execute()
        {
            var newPosition = new Vector2(
                Random.Range(-_boxSize.x*0.5f, _boxSize.x*0.5f),
                Random.Range(-_boxSize.y*0.5f, _boxSize.y*0.5f)
            );
            destinyVariable.Value = _originalPoint + newPosition;
            return NodeResult.success;
        }
    }
}
