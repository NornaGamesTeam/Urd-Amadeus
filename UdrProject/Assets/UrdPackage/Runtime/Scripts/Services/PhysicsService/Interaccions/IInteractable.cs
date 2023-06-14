using UnityEngine;

namespace Urd.Services.Physics
{
    public interface IInteractable
    {
        void Interact(Vector3 directionNormalized);
    }
}
