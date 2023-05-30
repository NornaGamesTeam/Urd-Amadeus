namespace Urd.Character
{
public class SparringCharacterController  : CharacterController
{
    protected override void Init()
    {
        base.Init();

        SetInput(new SparringCharacterInput(CharacterModel));
    }
}
}
