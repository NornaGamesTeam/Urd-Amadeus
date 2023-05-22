using UnityEngine;

namespace Urd.Services.Physics
{
    [System.Serializable]
    public class AreaShapeConeModel : AreaShapeModel
    {
        public override AreaShapeType AreaShape => AreaShapeType.Cone;

        [field: SerializeField]
        public float AngleDegreesClockWise { get; private set; }
        
        [field: SerializeField]
        public float Distance { get; private set; }

    }
}