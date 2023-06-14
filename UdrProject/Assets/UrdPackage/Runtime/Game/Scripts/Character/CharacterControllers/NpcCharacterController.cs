using UnityEngine;
using Urd.Services.Physics;

namespace Urd.Character
{
    public class NpcCharacterController : CharacterController<CharacterModel>, IInteractable
    {
        protected override void Init()
        {
            base.Init();

            SetInput(new NpcCharacterInput(CharacterModel));
        }

        public void Interact()
        {
            Debug.Log("Interact");
        }
    }
}