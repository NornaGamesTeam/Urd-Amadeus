using Urd.GameManager;
using Urd.Services;

namespace Urd.Game
{
    public interface IGameManagerService : IBaseService
    {
        T GetModule<T>() where T : class, IGameManagerModule;
        void CloseGame();
    }
}
