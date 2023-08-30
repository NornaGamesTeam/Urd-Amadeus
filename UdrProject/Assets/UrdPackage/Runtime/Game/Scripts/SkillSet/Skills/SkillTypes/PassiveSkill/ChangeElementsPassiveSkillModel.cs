using System;
using UnityEngine;

namespace Urd.Character.Skill
{
    [Serializable]
    public abstract class ChangeElementsPassiveSkillModel : PassiveSkillModel
    {
        [field: SerializeField, Header("Specific properties")]
        public ElementType Element { get; protected set; }

        [field: SerializeField, Range(0f,1f)]
        public float Factor { get; protected set; }
    }
}