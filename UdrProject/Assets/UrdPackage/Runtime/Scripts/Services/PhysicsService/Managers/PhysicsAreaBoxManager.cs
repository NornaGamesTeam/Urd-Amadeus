namespace Urd.Services.Physics
{
    [System.Serializable]
    public class PhysicsAreaBoxManager : PhysicsAreaShapeManager
    {
        public override AreaShapeType AreaShape => AreaShapeType.Box;
        public override bool TryHit(IAreaShapeModel areaShapeModel)
        {
            return false;
        }
    }
}
