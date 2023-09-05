using System;
using UnityEngine;

namespace Urd.Utils
{
    public static class DirectionUtils
    {
        private static readonly Vector2 Left = new Vector2(-1f, 0.26f);
        private static readonly Vector2 Up = new Vector2(0.03f, 0.5f);
        private static readonly Vector2 Right = new Vector2(1f, 0.26f);
        private static readonly Vector2 Down = new Vector2(0f, -0.5f);

        public static DirectionType ConvertToDirection(this Vector2 vector2)
        {
            DirectionType finalDirection = DirectionType.Left;
            float shortestDistance = (Left - vector2).sqrMagnitude;
            float distance = 0;

            distance = (Up - vector2).sqrMagnitude;
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                finalDirection = DirectionType.Up;
            }

            distance = (Right - vector2).sqrMagnitude;
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                finalDirection = DirectionType.Right;
            }

            distance = (Down - vector2).sqrMagnitude;
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                finalDirection = DirectionType.Down;
            }

            return finalDirection;
        }

        public static Vector2 ConvertToVector2(this DirectionType directionType)
        {
            switch (directionType)
            {
                case DirectionType.Up: return Vector2.up;
                case DirectionType.Down: return Vector2.down;
                case DirectionType.Left: return Vector2.left;
                case DirectionType.Right: return Vector2.right;
                default: return Vector3.zero;
            }
        }

    }

    [Serializable]
    public class OffsetDirectionParameter<T>
    {
        [field: SerializeField] public DirectionType Direction { get; private set; }
        [field: SerializeField] public T Item { get; private set; }
    }
    
    [Serializable]
    public class OffsetDirectionReference<T>
    {
        [field: SerializeField] public DirectionType Direction { get; private set; }
        [field: SerializeReference, SubclassSelector] public T Item { get; private set; }
    }
}
