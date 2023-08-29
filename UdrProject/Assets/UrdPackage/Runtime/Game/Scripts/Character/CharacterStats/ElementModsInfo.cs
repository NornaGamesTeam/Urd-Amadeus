using System;
using UnityEngine;
using Urd.Character.Skill;

namespace Urd.Character
{
    [Serializable]
    public class ElementModsInfo
    {
        [field:SerializeField]
        public ElementType Element { get; private set; }
        [field:SerializeField, Range(0f,1f)]
        public float Percentage { get; private set; }
    }
}