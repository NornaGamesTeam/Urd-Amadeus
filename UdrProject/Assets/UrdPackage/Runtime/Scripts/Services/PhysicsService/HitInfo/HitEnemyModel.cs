using UnityEngine;
using Urd.Utils;

namespace Urd.Services.Physics
{
    public class HitEnemyModel : HitModel
    {
        public HitEnemyModel(Vector2 position, Vector2 direction, IAreaShapeModel areaShapeModel) : base(
            position, direction, areaShapeModel, LayerMaskTypes.Enemy)
        {
        }
    }
}