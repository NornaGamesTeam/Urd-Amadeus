using UnityEngine;

namespace Urd.Character
{
    public class CharacterView : MonoBehaviour
    {
        private CharacterModel _characterModel;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            // remove this from the start
            SetModel(GetComponent<CharacterController>().CharacterModel);
        }

        private void SetModel(CharacterModel characterModel)
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