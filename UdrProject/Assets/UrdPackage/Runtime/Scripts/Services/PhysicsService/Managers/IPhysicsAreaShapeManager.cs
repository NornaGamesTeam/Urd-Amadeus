namespace Urd.Services.Physics
{
    public interface IPhysicsAreaShapeManager
    {
        AreaShapeType AreaShape { get; }
        bool TryHit(IHitModel hitModel);
    }
}
