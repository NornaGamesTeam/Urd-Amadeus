using UnityEngine;
using Urd.Utils;

namespace Urd.Services.Physics
{
    public class InteractNPCModel : HitModel
    {
        public InteractNPCModel(Vector2 position, Vector2 direction, IAreaShapeModel areaShapeModel) : base(
            position, direction, areaShapeModel, LayerMaskTypes.NPC)
        {
        }
    }
}