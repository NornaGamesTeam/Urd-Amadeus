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
            
            _characterModel.CharacterMovement.OnPositionChanged += OnCharacterMove;
            GetComponentInChildren<CharacterGraphicView>().SetModel(_characterModel);
        }
        
        private void OnCharacterMove(Vector2 characterPosition)
        {
            transform.position = characterPosition;
        }
    }
}