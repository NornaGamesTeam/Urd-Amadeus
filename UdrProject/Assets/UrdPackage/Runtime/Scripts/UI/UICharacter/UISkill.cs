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
            
        private ICharacterModel _characterModel;
        
        private ISkillModel _skillModel;

        private bool _isActive;

        public virtual void SetCharacterModel(ICharacterModel characterModel)
        {
            _characterModel = characterModel;

            _characterModel.SkillSetModel.OnSkillAction += OnSkillAction;
            _characterModel.SkillSetModel.OnIsSkill += OnIsSkill;
        }

        public void SetSkill(ISkillModel skillModel)
        {
            _skillModel = skillModel;
            Init();
        }
        
        private void Init()
        {
            _coolDownArea.sprite = _skillModel.UISkillConfig.Image;
            _background.sprite = _skillModel.UISkillConfig.BackgroundImage;
            EnableSkill(true);
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
            _background.color = enable? _skillModel.UISkillConfig.BackgroundColor :_skillModel.UISkillConfig.BackgroundColorCoolDown;
            _coolDownArea.color = enable? _skillModel.UISkillConfig.ImageColor : _skillModel.UISkillConfig.ImageColorCoolDown;
            _coolDownArea.fillAmount = enable?1:0;
        }

        private void BeginCooldown()
        {
            _isActive = false;
            _coolDownArea.DOFillAmount(1, _skillModel.CoolDown).
                          SetEase(_skillModel.UISkillConfig.AnimationEase)
                          .onComplete += OnCoolDownFinish;
        }

        private void OnCoolDownFinish()
        {
            EnableSkill(true);
        }
    }
}
