namespace Urd.Services.Physics
{
    public abstract class AreaShapeModel : IAreaShapeModel
    {
        public abstract AreaShapeType AreaShape { get; }
        
        public virtual bool Equals(IAreaShapeModel other)
        {
            return AreaShape == other.AreaShape;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AreaShapeModel)obj);
        }

        public override int GetHashCode()
        {
            return (int)AreaShape;
        }
    }
}