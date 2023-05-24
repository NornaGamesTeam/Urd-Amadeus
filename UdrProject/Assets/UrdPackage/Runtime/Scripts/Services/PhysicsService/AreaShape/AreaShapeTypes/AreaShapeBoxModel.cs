using UnityEngine;

namespace Urd.Services.Physics
{
    [System.Serializable]
    public class AreaShapeBoxModel : AreaShapeModel
    {
        public override AreaShapeType AreaShape => AreaShapeType.Box;

        [field: SerializeField]
        public Vector2 Area { get; private set; }

        public override bool Equals(IAreaShapeModel obj)
        {
            if (!(obj is AreaShapeBoxModel))
            {
                return false;
            }

            return base.Equals(obj) && ((AreaShapeBoxModel)obj).Area == Area;
        }
    }
}