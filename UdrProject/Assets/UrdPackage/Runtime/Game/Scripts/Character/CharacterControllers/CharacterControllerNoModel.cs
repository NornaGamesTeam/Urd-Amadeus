using UnityEngine;

namespace Urd.Character
{
    public abstract class CharacterControllerNoModel : MonoBehaviour, ICharacterController
    {
        public ICharacterModel CharacterModel { get; protected set; }

        protected abstract void Init();

        public virtual void Dispose() { }
    }
}