namespace Urd.Services.Physics
{
    [System.Serializable]
    public class PhysicsAreaConeManager : PhysicsAreaShapeManager
    {
        public override AreaShapeType AreaShape => AreaShapeType.Cone;
        public override bool TryHit(IAreaShapeModel areaShapeModel)
        {
            return false;
        }
    }
}
