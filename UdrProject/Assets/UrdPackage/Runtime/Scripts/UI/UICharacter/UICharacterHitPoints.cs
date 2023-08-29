using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Urd.Character;
using Urd.Character.Skill;

namespace Urd.UI
{
    public class UICharacterHitPoints : CharacterModelListener
    {
        private const string HIT_POINTS_FORMAT = "{0}/{1}";

        [SerializeField] private Image _background;
        [SerializeField] private Image _fillArea;

        [SerializeField] private TextMeshProUGUI _hitPointsText;

        [SerializeField] private bool _showOnlyIfNotFull;

        public override void Init()
        {
            base.Init();
            _characterModel.CharacterStatsModel.OnIsHit += OnIsHit;

            SetGameObjectsAs(!_showOnlyIfNotFull);

            _background.color = _characterModel.UICharacterConfig.HitPointsConfig.BackgroundColor;
            _fillArea.color = _characterModel.UICharacterConfig.HitPointsConfig.FillColor;
            _hitPointsText.color = _characterModel.UICharacterConfig.HitPointsConfig.TextColor;

            SetHitPoints();
        }

        private void OnIsHit(bool isHit, Vector2 hitDirection, ISkillModel hitModel)
        {
            if (isHit && !_characterModel.CharacterStatsModel.HasFullHitPoints && !_background.isActiveAndEnabled)
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

            var statsModel = _characterModel.CharacterStatsModel;

            _fillArea.fillAmount =
                statsModel.CurrentHitPoints / statsModel.MaxHitPoints;
            _hitPointsText.text =
                string.Format(HIT_POINTS_FORMAT, statsModel.CurrentHitPoints, statsModel.MaxHitPoints);
        }
    }
}
