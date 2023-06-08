using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Urd.Character;
using Urd.Character.Skill;

namespace Urd.UI
{
    public class UICharacterHitPoints : MonoBehaviour, IUICharacter
    {
        private const string HIT_POINTS_FORMAT = "{0}/{1}";
        
        [SerializeField]
        private Image _background;
        [SerializeField]
        private Image _fillArea;
        
        [SerializeField]
        private TextMeshProUGUI _hitPointsText;

        [SerializeField] private bool _showOnlyIfNotFull;
        
        private CharacterModel _characterModel;
     
        
        public void SetCharacterModel(CharacterModel characterModel)
        {
            _characterModel = characterModel;

            _characterModel.HitPointsModel.OnIsHit += OnIsHit;
            Init();
        }

        private void Init()
        {
            SetGameObjectsAs(!_showOnlyIfNotFull);
            
            _background.color = _characterModel.UICharacterConfig.HitPointsConfig.BackgroundColor;
            _fillArea.color = _characterModel.UICharacterConfig.HitPointsConfig.FillColor;
            _hitPointsText.color = _characterModel.UICharacterConfig.HitPointsConfig.TextColor;
            
            SetHitPoints();
        }

        private void OnIsHit(bool isHit, Vector2 hitDirection, ISkillModel hitModel)
        {
            if (isHit && !_characterModel.HitPointsModel.IsFull && !_background.isActiveAndEnabled)
            {
                SetGameObjectsAs(true);
            }
            SetHitPoints();
        }

        private void SetGameObjectsAs(bool enabled)
        {
            _background.gameObject.SetActive(enabled);
            _fillArea.gameObject.SetActive(enabled);
            _hitPointsText.gameObject.SetActive(enabled);
        }

        private void SetHitPoints()
        {
            // TODO do the animation
            
            _fillArea.fillAmount =
                _characterModel.HitPointsModel.HitPoints / _characterModel.HitPointsModel.MaxHitPoints;
            _hitPointsText.text = string.Format(HIT_POINTS_FORMAT, _characterModel.HitPointsModel.HitPoints,
                                                _characterModel.HitPointsModel.MaxHitPoints);
        }

    }
}
