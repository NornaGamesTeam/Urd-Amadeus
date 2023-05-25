using Urd.Utils;

namespace Urd.Services.Physics
{
    public class HitEnemyModel : HitModel
    {
        public override LayerMaskTypes LayerMask => LayerMaskTypes.Enemy;
        
        public HitEnemyModel(IAreaShapeModel areaShapeModel) : base(areaShapeModel) { }

    }
}