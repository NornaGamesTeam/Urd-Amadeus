using System;
using Urd.Services;

namespace Urd.Game
{
    [Serializable]
    public class GameManagerService : BaseService, IGameManagerService
    {
        public GameManagerCameraModule CameraModule { get; private set; }

        public override void Init()
        {
            base.Init();

            CameraModule = new GameManagerCameraModule();
            
            SetAsLoaded();
        }

       
    }
}