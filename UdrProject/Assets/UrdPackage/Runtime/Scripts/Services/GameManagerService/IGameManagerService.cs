using Urd.GameManager;
using Urd.Services;

namespace Urd.Game
{
    public interface IGameManagerService : IBaseService
    {
        GameManagerCameraModule CameraModule { get; }
        GameManagerDialogModule DialogModule { get; }
    }
}
