using UnityEngine;
using Urd.Services.Physics;

namespace Urd.Services
{
    public interface IPhysicsService : IBaseService
    {
        bool TryHit(Vector2 originPoint, Vector2 direction, IAreaShapeModel hitAreasActive);
    }
}