using UnityEngine;

namespace Urd.Services.Physics
{
    public interface IHittable
    {
        void Hit(float damage, Vector2 hitDirection);
    }
}