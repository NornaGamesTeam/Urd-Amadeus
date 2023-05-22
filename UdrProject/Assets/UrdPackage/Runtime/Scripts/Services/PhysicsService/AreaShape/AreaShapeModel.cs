namespace Urd.Services.Physics
{
    public abstract class AreaShapeModel : IAreaShapeModel
    {
        public abstract AreaShapeType AreaShape { get; }
    }
}