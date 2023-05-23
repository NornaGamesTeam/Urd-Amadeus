using UnityEngine;

namespace Urd.Services.Physics
{
    public abstract class PhysicsAreaShapeManager : IPhysicsAreaShapeManager
    {
        public abstract AreaShapeType AreaShape { get; }
        public abstract bool TryHit(Vector2 vector2, Vector2 originPoint, IAreaShapeModel areaShapeModel);
    }
}
