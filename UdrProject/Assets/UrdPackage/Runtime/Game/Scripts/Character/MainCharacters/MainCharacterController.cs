namespace Urd.Character
{
    public class MainCharacterController : CharacterController
    {
        protected override void Init()
        {
            base.Init();

            SetInput(new MainCharacterInput(CharacterModel));
        }
    }
}
