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
        private Image _fillArea;
        
        [SerializeField]
        private TextMeshProUGUI _hitPointsText;
            
        private CharacterModel _characterModel;
        
        public void SetCharacterModel(CharacterModel characterModel)
        {
            _characterModel = characterModel;

            _characterModel.HitPointsModel.OnIsHit += OnIsHit;
            SetHitPoints();
        }

        private void OnIsHit(bool isHit, Vector2 hitDirection, ISkillModel hitModel)
        {
            SetHitPoints();
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
