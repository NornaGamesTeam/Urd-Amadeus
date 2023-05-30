namespace Urd.Boomerang
{
    public class BoomerangInfoModel : BoomerangModel
    {
        public BoomerangInfoModel() : this(0) { }
        public BoomerangInfoModel(float duration) : base(BoomerangTypes.Info, duration) { }
    }
}