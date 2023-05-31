using DG.Tweening;
using MyBox;
using UnityEngine;
using UnityEngine.UI;
using Urd.Character;
using Urd.Character.Skill;

namespace Urd.UI
{
    public class UISkill : MonoBehaviour, IUICharacter
    {
        [SerializeField]
        private Image _background;
        
        [SerializeField]
        private Image _coolDownArea;
            
        private CharacterModel _characterModel;
        
        private ISkillModel _skillModel;

        // move this to a scriptable
        private Color _backgroundDefaultColor;
        private Color _coolDownAreaDefaultColor;

        private bool _isActive;
        
        protected virtual void Awake()
        {
            _backgroundDefaultColor = _background.color;
            _coolDownAreaDefaultColor= _coolDownArea.color;
            
            EnableSkill(true);
        }

        public virtual void SetCharacterModel(CharacterModel characterModel)
        {
            _characterModel = characterModel;

            _characterModel.SkillSetModel.OnSkillAction += OnSkillAction;
            _characterModel.SkillSetModel.OnIsSkill += OnIsSkill;
            EnableSkill(true);

        }

        public void SetSkill(ISkillModel skillModel)
        {
            _skillModel = skillModel;
        }

        private void OnSkillAction(ISkillModel skill)
        {
            if (skill.Name == _skillModel?.Name)
            {
                EnableSkill(false);
                _isActive = true;
            }
        }
        
        private void OnIsSkill(bool isSkill)
        {
            if (!isSkill && _isActive)
            {
                BeginCooldown();
            }
        }

        private void EnableSkill(bool enable)
        {
            _background.color = enable? _backgroundDefaultColor : Color.gray;
            _coolDownArea.color = enable? _coolDownAreaDefaultColor : Color.gray;
            _coolDownArea.fillAmount = enable?1:0;
        }

        private void BeginCooldown()
        {
            _isActive = false;
            _coolDownArea.DOFillAmount(1, _skillModel.CoolDown).onComplete += OnCoolDownFinish;
        }

        private void OnCoolDownFinish()
        {
            EnableSkill(true);
        }
    }
}
