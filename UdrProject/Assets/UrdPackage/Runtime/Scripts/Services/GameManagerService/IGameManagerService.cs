using Urd.GameManager;
using Urd.Services;

namespace Urd.Game
{
    public interface IGameManagerService : IBaseService
    {
        GameManagerConfig GameManagerConfig { get; }
        T GetModule<T>() where T : class, IGameManagerModule;
        void CloseGame();
        void NewGame();
        void LoadGame(GameDataModel gameDataModel);
    }
}
