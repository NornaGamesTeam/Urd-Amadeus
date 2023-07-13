using UnityEngine;

namespace Urd.Character
{
    public class CharacterView : CharacterModelListener
    {
        public override void Init()
        {
            // remove this from the start
            SetModel(_characterModel);
        }

        private void SetModel(ICharacterModel characterModel)
        {
            _characterModel = characterModel;
            
            _characterModel.CharacterMovement.OnPhysicPositionChanged += OnPhysicCharacterMove;
            GetComponentInChildren<CharacterGraphicView>().SetModel(_characterModel);
        }
        
        private void OnPhysicCharacterMove(Vector2 characterPosition)
        {
            transform.position = characterPosition;
        }
    }
}