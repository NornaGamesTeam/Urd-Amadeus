using UnityEngine;
using Urd.Services.Physics;

namespace Urd.Character
{
    public class SparringCharacterController : CharacterController<CharacterModel>, IHittable
    {
        protected override void Init()
        {
            base.Init();

            SetInput(new SparringCharacterInput(CharacterModel));
        }

        public void Hit(float damage, Vector2 hitDirection)
        {
            _hitPointsController.Hit(damage, hitDirection);
        }
    }
}