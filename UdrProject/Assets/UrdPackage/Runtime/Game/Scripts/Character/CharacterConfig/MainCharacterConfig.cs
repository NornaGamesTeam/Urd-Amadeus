using UnityEngine;
using Urd.Services.Physics;

namespace Urd.Character
{
    public class MainCharacterConfig : CharacterConfig
    {
        [field: Header("InteractionObject"), SerializeField] 
        public float InteractionObjectDistance { get; private set; }
        
        [field: SerializeField] 
        public AreaShapeSphereModel InteractionArea  { get; private set; }
    }
}
