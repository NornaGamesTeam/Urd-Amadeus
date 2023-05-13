namespace Urd.Character
{
    public class MainCharacterController : CharacterController
    {
        protected override void Init()
        {
            base.Init();

            _characterMovementController.SetInput(new MainCharacterInput(CharacterModel));
        }
    }
}
