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

        private Vector2 _lastPosition;
        
        public void SetModel(CharacterModel characterModel)
        {
            _characterModel = characterModel;

            Init();
        }

        private void Init()
        {
            _characterModel.Movement.OnPositionChanged += OnPositionChanged;
            _characterModel.Movement.OnIsMovingChanged += OnMovingChanged;
        }

        private void OnMovingChanged(bool isMoving)
        {
            _animator.SetBool(ANIMATION_KEY_MOVEMENT_WALKING, isMoving);
        }

        private void OnPositionChanged(Vector2 newPosition)
        {
            var deltaPosition = newPosition - _lastPosition;

            _animator.SetFloat(ANIMATION_KEY_MOVEMENT_X, deltaPosition.x);
            _animator.SetFloat(ANIMATION_KEY_MOVEMENT_Y, deltaPosition.y);
            
            _lastPosition = newPosition;
        }
    }
}
