using UnityEngine;
using Urd.Character.Skill;

namespace Urd.Character
{
    public class CharacterGraphicView : MonoBehaviour
    {
        private const string ANIMATION_KEY_MOVEMENT_X = "MOVEMENT_X";
        private const string ANIMATION_KEY_MOVEMENT_Y = "MOVEMENT_Y";
        private const string ANIMATION_KEY_IS_MOVING = "IS_MOVING";
        private const string ANIMATION_KEY_IS_SKILL = "IS_SKILL";
        
        [SerializeField]
        private SpriteRenderer _mainImage;
        [SerializeField]
        private Animator _animator;

        private CharacterModel _characterModel;
        private CharacterAnimParameters _lastAnimParameter = CharacterAnimParameters.None;
        
        public void SetModel(CharacterModel characterModel)
        {
            _characterModel = characterModel;

            Init();
        }

        private void Init()
        {
            _characterModel.CharacterMovement.OnRawNormalizedMovementChanged += OnRawNormalizedPositionChanged;
            _characterModel.CharacterMovement.OnIsMovingChanged += OnMovingChanged;
            _characterModel.SkillSetModel.OnIsSkill += OnIsSkill;
            _characterModel.SkillSetModel.OnSkillAction += OnSkillAction;
        }

        private void OnSkillAction(ISkillModel skillModel)
        {
            _animator.SetBool(skillModel.AnimParameter.ToString(), true);
            _lastAnimParameter = skillModel.AnimParameter;
        }

        private void OnIsSkill(bool onIsSkill)
        {
            _animator.SetBool(ANIMATION_KEY_IS_SKILL, onIsSkill);
            if (!onIsSkill && _lastAnimParameter != CharacterAnimParameters.None)
            {
                _animator.SetBool(_lastAnimParameter.ToString(), false);
                _lastAnimParameter = CharacterAnimParameters.None;
            }
        }

        private void OnMovingChanged(bool isMoving)
        {
            _animator.SetBool(ANIMATION_KEY_IS_MOVING, isMoving);
        }

        private void OnRawNormalizedPositionChanged(Vector2 newRawNormalizedPosition)
        {
            _animator.SetFloat(ANIMATION_KEY_MOVEMENT_X, newRawNormalizedPosition.x);
            _animator.SetFloat(ANIMATION_KEY_MOVEMENT_Y, newRawNormalizedPosition.y);
        }
    }
}
