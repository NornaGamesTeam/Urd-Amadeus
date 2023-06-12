using System;
using Urd.GameManager;
using Urd.Services;

namespace Urd.Game
{
    [Serializable]
    public class GameManagerService : BaseService, IGameManagerService
    {
        public GameManagerCameraModule CameraModule { get; private set; }
        public GameManagerDialogModule DialogModule { get; private set; }

        public override void Init()
        {
            base.Init();

            CameraModule = new ();
            DialogModule = new ();
            
            SetAsLoaded();
        }

       
    }
}