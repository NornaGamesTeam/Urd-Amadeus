using Urd.Game;

namespace Urd.GameManager
{
    public abstract class GameManagerModule : IGameManagerModule
    {
        protected GameManagerConfig _gameManagerConfig;

        protected GameDataModel _gameDataModel;
        
        public virtual void Init(GameManagerConfig gameManagerConfig)
        {
            _gameManagerConfig = gameManagerConfig;
        }

        public virtual void LoadGame(GameDataModel gameDataModel)
        {
            _gameDataModel = gameDataModel;
        }
    }
}