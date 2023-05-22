using System;
using System.Collections.Generic;
using UnityEngine;
using Urd.Utils;

namespace Urd.Game.SkillTrees
{
    [Serializable]
    public class HitAreaByDirection
    {
        [field: SerializeField] 
        public DirectionType Direction { get; private set; }
        [field: SerializeReference, SubclassSelector] 
        public List<HitAreaModel> HitArea { get; private set; }
    }
}
