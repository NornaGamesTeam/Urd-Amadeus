using Urd.Game.Camera;
using Urd.Services;

namespace Urd.Game
{
    public interface IGameManagerService : IBaseService
    {
        GameCameraModel GameCameraModel { get; }
    }
}
