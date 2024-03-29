using MyBox;
using UnityEngine;
using UnityEngine.Serialization;
using Urd.Character.Skill;

namespace Urd.Character
{
    public class CharacterGraphicView : MonoBehaviour
    {
        // movement related
        private const string ANIMATION_KEY_MOVEMENT_X = "MOVEMENT_X";
        private const string ANIMATION_KEY_MOVEMENT_Y = "MOVEMENT_Y";
        private const string ANIMATION_KEY_AIM_X = "AIM_X";
        private const string ANIMATION_KEY_AIM_Y = "AIM_Y";
        private const string ANIMATION_KEY_IS_MOVING = "IS_MOVING";
        private const string ANIMATION_KEY_IS_SKILL = "IS_SKILL";
        
        // hit related
        private const string ANIMATION_KEY_IS_HIT = "IS_HIT";
        
        [SerializeField]
        private SpriteRenderer _mainImage;
        
        [SerializeField]
        private Animator _animator;
        
        [Header("AIM")]
        [SerializeField]
        private SpriteRenderer _debugAimImage;
        [SerializeField]
        private float _debugAimOffset;

        private ICharacterModel _characterModel;
        private string _lastAnimation;
        
        public void SetModel(ICharacterModel characterModel)
        {
            _characterModel = characterModel;

            Init();
        }

        private void Init()
        {
            // movement related
            _characterModel.MovementModel.OnRawNormalizedMovementChanged += OnRawNormalizedPositionChanged;
            _characterModel.MovementModel.OnIsMovingChanged += OnMovingChanged;
            _characterModel.MovementModel.OnAimDirectionChanged += OnAimDirectionChanged;
            _characterModel.SkillSetModel.OnIsDoingSkill += OnIsDoingSkill;
            _characterModel.SkillSetModel.OnSkillAction += OnSkillAction;
            
            // hitted related
            _characterModel.CharacterStatsModel.OnIsHit += OnIsHit;
            
            // Interact related
            var npcModel = _characterModel as NpcCharacterModel;
            if (npcModel != null)
            {
                npcModel.NPCInteractionsModel.OnInteract += OnInteract;
            }
        }

        private void OnInteract(Vector2 interactDirection)
        {
            OnAimDirectionChanged(interactDirection);
        }

        private void OnIsHit(bool isHit, Vector2 hitDirection, ISkillModel hitSkillModel)
        {
            _animator.SetBool(ANIMATION_KEY_IS_HIT, isHit);
            _animator.Play(hitSkillModel.SkillAnimationModel.AnimationName.ToString());

            OnAimDirectionChanged(hitDirection);
        }

        private void OnSkillAction(ISkillModel skillModel)
        {
            _animator.Play(skillModel.SkillAnimationModel.AnimationName.ToString());
        }

        private void OnIsDoingSkill(bool onIsSkill)
        {
            _animator.SetBool(ANIMATION_KEY_IS_SKILL, onIsSkill);
            if (!onIsSkill && !string.IsNullOrEmpty(_lastAnimation))
            {
                _animator.SetBool(_lastAnimation, false);
                _lastAnimation = null;
            }
        }

        private void OnMovingChanged(bool isMoving)
        {
            _animator.SetBool(ANIMATION_KEY_IS_MOVING, isMoving);
        }
        
        private void OnAimDirectionChanged(Vector2 aimDirection)
        {
            _debugAimImage.transform.SetXY(
                transform.position.x + aimDirection.x * _debugAimOffset,
                transform.position.y + aimDirection.y * _debugAimOffset);
            
            _animator.SetFloat(ANIMATION_KEY_AIM_X, aimDirection.x);
            _animator.SetFloat(ANIMATION_KEY_AIM_Y, aimDirection.y);
        }

        private void OnRawNormalizedPositionChanged(Vector2 newRawNormalizedPosition)
        {
            _animator.SetFloat(ANIMATION_KEY_MOVEMENT_X, newRawNormalizedPosition.x);
            _animator.SetFloat(ANIMATION_KEY_MOVEMENT_Y, newRawNormalizedPosition.y);
        }
    }
}
