using UnityEngine;

namespace Urd.Game.Camera
{
    public class GameCameraConfig : ScriptableObject
    {
        [field: SerializeField, Header("Camera Properties")]
        public float OrthographicSize { get; private set; }
        
        [field: SerializeField, Header("Camera Behavior")]
        public float TimeToReachTheObjective { get; private set; }
    }
}