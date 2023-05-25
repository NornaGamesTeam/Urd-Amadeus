using System;
using System.Collections.Generic;
using UnityEngine;
using Urd.Utils;

namespace Urd.Game.SkillTrees
{
    [Serializable]
    public class AttackAreaByDirection
    {
        [field: SerializeField] 
        public DirectionType Direction { get; private set; }
        [field: SerializeField] 
        public List<AttackAreaModel> HitArea { get; private set; }
        
    }
}
