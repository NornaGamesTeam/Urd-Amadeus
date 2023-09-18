using UnityEngine;
using Urd.GameManager;
using Urd.Utils;

namespace Urd.Game.Camera
{
    public class GameCameraView : MonoBehaviour
    {
        private UnityEngine.Camera _camera;
        
        private GameCameraModel _gameCameraModel;
        private void Start()
        {
            Init();
        }

        private void Init()
        {
            _camera = GetComponent<UnityEngine.Camera>();
                
            _gameCameraModel = StaticServiceLocator.Get<IGameManagerService>().
                                                    GetModule<GameManagerCameraModule>().GameCameraModel;
            _gameCameraModel.OnPositionChagned += OnGameCameraPositionChanged;

            SetUpCamera();
        }

        private void SetUpCamera()
        {
            _camera.orthographicSize = _gameCameraModel.OrthographicSize;
        }

        private void OnDisable()
        {
            _gameCameraModel.OnPositionChagned -= OnGameCameraPositionChanged;
        }
        
        private void OnGameCameraPositionChanged(Vector2 cameraPosition)
        {
            transform.position = cameraPosition;
        }
    }
}