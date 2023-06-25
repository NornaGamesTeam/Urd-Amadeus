using MBT;
using UnityEngine;

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

        private float _movementDuration;

        public override void OnEnter()
        {
            base.OnEnter();
            
            var characterPosition = characterVariable.Value.CharacterModel.CharacterMovement.Position;
            _movementDuration = Vector2.Distance(characterPosition, destinyVariable.Value) /
                                characterVariable.Value.CharacterModel.CharacterMovement.Speed;
        }

        public override NodeResult Execute()
        {
            var characterPosition = characterVariable.Value.CharacterModel.CharacterMovement.Position;

            if (Vector2.Distance(characterPosition, destinyVariable.Value) < 0.01f)
            {
                return NodeResult.success;
            }
            var timestamp = DeltaTime;
            
            Vector2 newPosition =
                Vector2.Lerp(characterPosition, destinyVariable.Value, timestamp / _movementDuration);
            characterVariable.Value.CharacterModel.CharacterMovement.SetPosition(newPosition);
            
            return NodeResult.running;
        }
    }
}
