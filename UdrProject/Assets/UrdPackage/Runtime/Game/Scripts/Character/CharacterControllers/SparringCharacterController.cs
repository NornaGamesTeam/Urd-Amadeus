using UnityEngine;
using Urd.Services.Physics;

namespace Urd.Character
{
    public class SparringCharacterController : CharacterController<CharacterModel>, IHittable
    {
        protected override void Init()
        {
            base.Init();

            SetInput(new EnemyCharacterInput(CharacterModel));
        }

        public void Hit(float damage, Vector2 hitDirection)
        {
            StatsController.Hit(damage, hitDirection);
        }
    }
}