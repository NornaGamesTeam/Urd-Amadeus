using UnityEngine;
using Urd.Utils.Game.Physics;

namespace Urd.World
{
    public class EnterArea : OnTriggerDetection
    {
        [field: SerializeField]
        public WorldAreaTypes DestinyWorldArea { get; private set; }

        void Init()
        {
            OnTriggerEnter += OnEnterInArea;
        }

        private void OnEnterInArea(Collider2D collider2D)
        {
            Debug.Log($"OnEnterInArea {collider2D.name}");
        }
    }
}