using Urd.Game.SkillTrees;

namespace Urd.Services.Physics
{
    public abstract class PhysicsAreaShapeManager : IPhysicsAreaShapeManager
    {
        public abstract AreaShapeType AreaShape { get; }
        public abstract bool TryHit(IAreaShapeModel areaShapeModel);
    }
}
