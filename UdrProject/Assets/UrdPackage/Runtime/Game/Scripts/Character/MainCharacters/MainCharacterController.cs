namespace Urd.Character
{
    public class MainCharacterController : CharacterController
    {
        protected override void Init()
        {
            base.Init();

            _movementController.SetInput(new MainCharacterInput(CharacterModel));
        }
    }
}
