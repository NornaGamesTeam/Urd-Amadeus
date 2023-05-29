using UnityEngine;
using Urd.Utils;

namespace Urd.Services.Physics
{
    public class HitEnemyModel : HitModel
    {
        public override LayerMaskTypes LayerMask => LayerMaskTypes.Enemy;

        public HitEnemyModel(Vector2 position, Vector2 direction, IAreaShapeModel areaShapeModel) : base(
            position, direction, areaShapeModel)
        {
        }
    }
}