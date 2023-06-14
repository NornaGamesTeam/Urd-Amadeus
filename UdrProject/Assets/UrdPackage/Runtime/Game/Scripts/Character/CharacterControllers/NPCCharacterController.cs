using Urd.Services.Physics;

namespace Urd.Character
{
    public class NPCCharacterController : CharacterController<CharacterModel>, IInteractable
    {
        protected override void Init()
        {
            base.Init();

            SetInput(new NPCSparringCharacterInput(CharacterModel));
        }

        public void Interact()
        {
            
        }
    }
}