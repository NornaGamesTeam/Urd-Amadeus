using UnityEngine;

namespace Urd.Character
{
    public class CharacterGraphicView : MonoBehaviour
    {
        private const string ANIMATION_KEY_MOVEMENT_X = "MOVEMENT_X";
        private const string ANIMATION_KEY_MOVEMENT_Y = "MOVEMENT_Y";
        private const string ANIMATION_KEY_MOVEMENT_WALKING = "MOVEMENT_IS_WALKING";
        
        [SerializeField]
        private SpriteRenderer _mainImage;
        [SerializeField]
        private Animator _animator;

        private CharacterModel _characterModel;
        
        public void SetModel(CharacterModel characterModel)
        {
            _characterModel = characterModel;

            Init();
        }

        private void Init()
        {
            _characterModel.CharacterMovement.OnRawNormalizedMovementChanged += OnRawNormalizedPositionChanged;
            _characterModel.CharacterMovement.OnIsMovingChanged += OnMovingChanged;
        }

        private void OnMovingChanged(bool isMoving)
        {
            _animator.SetBool(ANIMATION_KEY_MOVEMENT_WALKING, isMoving);
        }

        private void OnRawNormalizedPositionChanged(Vector2 newRawNormalizedPosition)
        {
            _animator.SetFloat(ANIMATION_KEY_MOVEMENT_X, newRawNormalizedPosition.x);
            _animator.SetFloat(ANIMATION_KEY_MOVEMENT_Y, newRawNormalizedPosition.y);
        }
    }
}
