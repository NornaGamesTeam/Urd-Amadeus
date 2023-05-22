using Urd.Services.Physics;

namespace Urd.Services
{
    public interface IPhysicsService : IBaseService
    {
        bool TryHit(IAreaShapeModel hitAreasActive);
    }
}