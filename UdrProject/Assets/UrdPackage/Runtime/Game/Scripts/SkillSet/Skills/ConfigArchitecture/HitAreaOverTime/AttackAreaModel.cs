using UnityEngine;
using Urd.Services.Physics;

namespace Urd.Game.SkillTrees
{
    [System.Serializable]
    public class AttackAreaModel
    {
        [field: SerializeField]
        public float BeginTime { get; private set; }
        [field: SerializeField]
        public float EndTime { get; private set; }
        
        [field: SerializeField, Range(0,1)]
        public float DamagePercentage { get; private set; }

        [field: SerializeField] 
        public Vector2 OriginPoinOffset { get; private set; }
        [field: SerializeField] 
        public float RotationDegreesClockWise { get; private set; }
        
        [field: SerializeReference, SubclassSelector]
        public IAreaShapeModel AreaShapeModel { get; private set; }

    }
}