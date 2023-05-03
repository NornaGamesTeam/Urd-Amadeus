using UnityEngine;

namespace Urd.Character
{
    public class CharacterConfig : ScriptableObject
    {
        [field: SerializeField]
        public float Speed { get; private set; }
    }
}
