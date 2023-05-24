using UnityEngine;

namespace Urd.Utils
{
    public static class VectorUtils
    {
        public static Vector3 RotateDegrees(this Vector3 vector, float degrees)
        {
            return Quaternion.AngleAxis(degrees, Vector3.forward) * vector;
        }
        
        public static Vector2 RotateDegrees(this Vector2 vector, float degrees)
        {
            return Quaternion.AngleAxis(degrees, Vector3.forward) * vector;
        }
    }
}