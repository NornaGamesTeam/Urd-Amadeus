using TMPro;
using UnityEngine;
using Urd.View.Boomerang;

namespace Urd.Boomerang
{
    public class BoomerangHitDamageView : BoomerangView<BoomerangHitDamageModel>
    {
        [SerializeField]
        public TextMeshPro _text;

        protected override void OnBeginOpen()
        {
            base.OnBeginOpen();
            _text.text = $"{Model.Damage}";
            _text.color = Model.TextColor;
        }
    }
}