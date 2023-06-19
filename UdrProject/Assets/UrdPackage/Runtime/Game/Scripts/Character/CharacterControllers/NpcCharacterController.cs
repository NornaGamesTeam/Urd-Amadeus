using UnityEngine;
using Urd.Services.Physics;

namespace Urd.Character
{
    public class NpcCharacterController : CharacterController<NpcCharacterModel>, IInteractable
    {
        protected NpcInteractionsController _npcInteractionsController;

        protected override void Init()
        {
            base.Init();

            SetInput(new NpcCharacterInput(CharacterModel));
        }

        public override void SetInput(ICharacterInput characterInput)
        {
            base.SetInput(characterInput);

            _npcInteractionsController = new NpcInteractionsController(CharacterModel);
        }

        public void Interact(Vector3 directionNormalized)
        {
            _npcInteractionsController.Interact(directionNormalized);
        }

        public void ShowInteractButton(bool showInteractButton)
        {
            (CharacterModel as NpcCharacterModel).NPCInteractionsModel.SetAsAbleToTalk(showInteractButton);
        }
    }
}