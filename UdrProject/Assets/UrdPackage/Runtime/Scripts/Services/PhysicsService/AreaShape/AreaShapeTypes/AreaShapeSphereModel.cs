using UnityEngine;

namespace Urd.Services.Physics
{
    [System.Serializable]
    public class AreaShapeSphereModel : AreaShapeModel
    {
        public override AreaShapeType AreaShape => AreaShapeType.Sphere;

        [field: SerializeField]
        public float Radio { get; private set; }

        public override bool Equals(IAreaShapeModel obj)
        {
            if (!(obj is AreaShapeSphereModel))
            {
                return false;
            }

            return base.Equals(obj) && ((AreaShapeSphereModel)obj).Radio == Radio;
        }
    }
}