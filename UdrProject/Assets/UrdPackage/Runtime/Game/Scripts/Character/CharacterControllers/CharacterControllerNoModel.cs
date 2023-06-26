using MyBox;
using UnityEngine;
using UnityEngine.Serialization;

namespace Urd.Character
{
    public abstract class CharacterControllerNoModel : MonoBehaviour, ICharacterController
    {
        [field: SerializeField, ReadOnly]
        public ICharacterModel CharacterModel { get; protected set; }

        [field: SerializeField, ReadOnly]
        public ICharacterInput CharacterInput { get; protected set; }

        protected abstract void Init();

        public virtual void Dispose() { }
        
        public virtual void SetInput(ICharacterInput characterInput)
        {
            // TODO move this to other place
            CharacterInput = characterInput;
        }
    }
}