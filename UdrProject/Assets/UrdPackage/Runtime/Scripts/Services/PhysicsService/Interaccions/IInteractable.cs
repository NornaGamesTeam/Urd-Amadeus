using UnityEngine;

namespace Urd.Services.Physics
{
    public interface IInteractable
    {
        void Interact(Vector3 directionNormalized);
        void ShowInteractButton(bool showInteractButton);
    }
}
