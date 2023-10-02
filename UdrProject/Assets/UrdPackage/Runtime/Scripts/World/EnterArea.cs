using System;
using UnityEngine;
using Urd.Game;
using Urd.GameManager;
using Urd.Utils;
using Urd.Utils.Game.Physics;

namespace Urd.World
{
    public class EnterArea : OnTriggerDetection
    {
        [field: SerializeField]
        public WorldAreaTypes DestinyWorldArea { get; private set; }

        public void Init()
        {
            OnTriggerEnter += OnEnterInArea;
        }

        private void OnEnterInArea(Collider2D collider2D)
        {
            var gameManagerModule = StaticServiceLocator.Get<IGameManagerService>()
                                                        .GetModule<GameManagerWorldManagerModule>();
            gameManagerModule.LoadMap(DestinyWorldArea);
            Debug.Log($"OnEnterInArea {collider2D.name}");
        }
    }
}