using Urd.Game.SkillTrees;

namespace Urd.Services.Physics
{
    public interface IPhysicsAreaShapeManager
    {
        AreaShapeType AreaShape { get; }
        bool TryHit(IAreaShapeModel areaShapeModel);
    }
}
