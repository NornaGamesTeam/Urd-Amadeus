using Urd.Game;

namespace Urd.GameManager
{
    public interface IGameManagerModule
    {
        void Init(GameManagerConfig gameManagerConfig);
        void LoadGame(GameDataModel gameDataModel);
    }
}
