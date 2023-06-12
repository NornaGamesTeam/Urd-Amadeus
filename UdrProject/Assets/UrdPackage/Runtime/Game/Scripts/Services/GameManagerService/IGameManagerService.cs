using Urd.Services;

namespace Urd.Game
{
    public interface IGameManagerService : IBaseService
    {
        GameManagerCameraModule CameraModule { get;  }
    }
}
