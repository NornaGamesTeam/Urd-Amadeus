using UnityEngine;

namespace Urd.Character
{
    public class CharacterView : MonoBehaviour
    {
        private ICharacterModel _characterModel;

        [SerializeReference] 
        private CharacterController<CharacterModel> _characterController;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            // remove this from the start
            SetModel(_characterController.CharacterModel);
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