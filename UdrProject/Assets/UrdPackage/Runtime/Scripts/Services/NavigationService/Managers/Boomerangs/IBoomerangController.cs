using Urd.View.Boomerang;

namespace Urd.Boomerang
{
    public interface IBoomerangController
    {
        BoomerangBodyView BoomerangDefaultBody { get; }
        BoomerangBodyView BoomerangBody { get; }
        void SetBoomerangBody(BoomerangBodyView boomerangBody);
        void Open();
        void Close();
    }
}