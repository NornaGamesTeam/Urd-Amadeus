using UnityEngine;

namespace Urd.Character
{
    public class CharacterGraphicsConfig : ScriptableObject
    {
        
        [field: SerializeField]
        public float RangeToConsiderWalking { get; private set; }
        
    }
}
