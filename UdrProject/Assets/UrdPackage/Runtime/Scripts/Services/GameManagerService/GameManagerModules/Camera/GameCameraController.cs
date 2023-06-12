using System;
using UnityEngine;
using Urd.Character;
using Urd.Services;
using Urd.Utils;

namespace Urd.Game.Camera
{
    public class GameCameraController : IDisposable
    {
        private const string CAMERA_CONFIG_PATH = "GameCamera/GameCameraConfig";
        
        private GameCameraModel _gameCameraModel;
        private GameCameraConfig _gameCameraConfig;

        private MainCharacterController _mainCharacterController;

        public GameCameraController(GameCameraModel gameCameraModel)
        {
            _gameCameraModel = gameCameraModel;
            LoadData();
            _gameCameraModel.SetConfig(_gameCameraConfig);
        }

        public void Init()
        {
            FollowPlayer();
        }

        public void Dispose()
        {
            StaticServiceLocator.Get<IClockService>().UnSubscribeToUpdate(CustomUpdate);
        }
        private void LoadData()
        {
            _gameCameraConfig = UnityEngine.Resources.Load<GameCameraConfig>(CAMERA_CONFIG_PATH);
        }
        
        private void FollowPlayer()
        {
            // TODO do it better
            _mainCharacterController = GameObject.FindObjectOfType<MainCharacterController>();

            StaticServiceLocator.Get<IClockService>()?.SubscribeToUpdate(CustomUpdate);
        }

        private void CustomUpdate(float deltaTime)
        {
            _gameCameraModel.SetPosition(_mainCharacterController.CharacterModel.CharacterMovement.Position);
        }
    }
}