using System;
using System.Linq;
using DG.DemiEditor;
using MBT;
using UnityEngine;
using Random = UnityEngine.Random;

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
        
        [Header("Editor"), SerializeField]
        private bool _drawGizmos;
        
        private void Start()
        {
            _originalPoint = characterVariable.Value.CharacterModel.MovementModel.PhysicPosition;
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

        private void OnDrawGizmos()
        {
            if (!_drawGizmos)
            {
                return;
            }

            Vector2 position = _originalPoint;
            if (position == Vector2.zero)
            {
                position = transform.position;
            }
            Gizmos.color = Color.white.SetAlpha(0.5f);; 
            Gizmos.DrawCube(position, _boxSize);
            Gizmos.color = Color.black.SetAlpha(0.5f);
            if (destinyVariable != null 
                && destinyVariable.blackboard.GetVariable<BlackboardVariable>(destinyVariable.key) != null)
            {
                Gizmos.DrawSphere(destinyVariable.Value, 0.5f);
            }
        }
    }
}
