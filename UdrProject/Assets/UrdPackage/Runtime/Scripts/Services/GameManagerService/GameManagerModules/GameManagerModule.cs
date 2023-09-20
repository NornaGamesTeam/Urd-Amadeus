namespace Urd.GameManager
{
    public abstract class GameManagerModule : IGameManagerModule
    {
        protected GameManagerConfig _gameManagerConfig;
        
        public virtual void Init(GameManagerConfig gameManagerConfig)
        {
            _gameManagerConfig = gameManagerConfig;
        }
    }
}