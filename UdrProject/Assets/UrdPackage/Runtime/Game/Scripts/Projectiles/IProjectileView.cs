using System;

namespace Urd.Game.Projectile
{
    public interface IProjectileView : IDisposable
    {
        void SetUp(IProjectileModel projectileModel);
    }
}