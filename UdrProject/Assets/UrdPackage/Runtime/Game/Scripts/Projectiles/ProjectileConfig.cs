using UnityEngine;

namespace Urd.Game.Projectile
{
    [CreateAssetMenu(fileName = "NewProjectileConfig", menuName = "Urd/Projectile/New ProjectileConfig", order = 1)]
    public class ProjectileConfig : ScriptableObject
    {
        [field: SerializeReference, SubclassSelector] public IProjectileController ProjectileController { get; private set; }
        [field: SerializeReference, SubclassSelector] public IProjectileModel ProjectileModel { get; private set; }
    }
}