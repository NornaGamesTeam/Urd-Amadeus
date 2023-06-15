using UnityEngine;
using Urd.Utils;

namespace Urd.Services.Physics
{
    public class InteractNPCModel : HitModel
    {
        public override LayerMaskTypes LayerMask => LayerMaskTypes.NPC;

        public InteractNPCModel(Vector2 position, Vector2 direction, IAreaShapeModel areaShapeModel) : base(
            position, direction, areaShapeModel)
        {
        }
    }
}