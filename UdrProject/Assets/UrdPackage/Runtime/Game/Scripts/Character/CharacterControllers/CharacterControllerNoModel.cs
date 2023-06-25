using MyBox;
using UnityEngine;

namespace Urd.Character
{
    public abstract class CharacterControllerNoModel : MonoBehaviour, ICharacterController
    {
        [SerializeField, ReadOnly]
        public ICharacterModel CharacterModel { get; protected set; }

        protected abstract void Init();

        public virtual void Dispose() { }
    }
}