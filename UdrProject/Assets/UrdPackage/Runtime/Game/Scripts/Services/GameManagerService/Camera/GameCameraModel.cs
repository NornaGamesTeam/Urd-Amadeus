using System;
using UnityEngine;

namespace Urd.Game.Camera
{
    public class GameCameraModel
    {
        public float OrthographicSize => _gameCameraConfig.OrthographicSize;
        public Vector2 Position { get; private set; }

        private GameCameraConfig _gameCameraConfig;

        public event Action<Vector2> OnPositionChagned;

        public void SetPosition(Vector2 newPosition)
        {
            if (newPosition == Position)
            {
                return;
            }

            Position = newPosition;
            OnPositionChagned?.Invoke(Position);
        }

        public void SetConfig(GameCameraConfig gameCameraConfig)
        {
            _gameCameraConfig = gameCameraConfig;
        }
    }
}
