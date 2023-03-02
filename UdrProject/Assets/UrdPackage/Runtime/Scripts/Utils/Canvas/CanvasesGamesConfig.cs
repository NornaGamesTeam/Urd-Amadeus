using UnityEngine;

namespace Urd.Utils
{
    public class CanvasesGamesConfig : ScriptableObject
    {
        public enum CanvasOrientation
        {
            Landscape,
            Portrait
        }

        [field: SerializeField] public CanvasOrientation GameOrientation { get; private set; }

        [field: SerializeField] public Vector2 CanvasResolution { get; private set; }
    }
}