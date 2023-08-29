using System;
using UnityEngine;
using Urd.Game.SkillTrees;
using Urd.Services.Physics;

namespace Urd.Character.Skill
{
    [Serializable]
    public class InteractSkillModel : SkillModel<SkillConfig>
    {
        [field: SerializeField, Header("Specific Properties")]
        public float Distance { get; private set; }
        
        [field: SerializeField]
        public AreaShapeSphereModel AreaSphere { get; private set; }
    }
}