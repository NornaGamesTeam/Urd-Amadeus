namespace Urd.Character
{
    public class MainCharacterController : CharacterController<CharacterModel>
    {
        protected override void Init()
        {
            base.Init();

            SetInput(new MainCharacterInput(CharacterModel));
        }
    }
}
