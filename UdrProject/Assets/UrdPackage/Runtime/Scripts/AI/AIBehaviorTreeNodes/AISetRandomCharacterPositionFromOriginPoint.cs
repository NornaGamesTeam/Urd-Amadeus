using System;
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
        private Vector2Reference _destinyVariable = new Vector2Reference(VarRefMode.DisableConstant);

        private Vector2 _originalPoint;
        
        [Header("Editor"), SerializeField]
        private bool _drawGizmos;
        
        private void Start()
        {
            _originalPoint = characterVariable.Value.CharacterModel.CharacterMovement.PhysicPosition;
        }

        public override NodeResult Execute()
        {
            var newPosition = new Vector2(
                Random.Range(-_boxSize.x*0.5f, _boxSize.x*0.5f),
                Random.Range(-_boxSize.y*0.5f, _boxSize.y*0.5f)
            );
            _destinyVariable.Value = _originalPoint + newPosition;
            return NodeResult.success;
        }

        private void OnDrawGizmos()
        {
            if (!_drawGizmos)
            {
                return;
            }

            Gizmos.color = Color.white.SetAlpha(0.5f);; 
            Gizmos.DrawCube(_originalPoint, _boxSize);
            Gizmos.color = Color.black.SetAlpha(0.5f);
            Gizmos.DrawSphere(_destinyVariable.Value, 0.5f);
        }
    }
}
