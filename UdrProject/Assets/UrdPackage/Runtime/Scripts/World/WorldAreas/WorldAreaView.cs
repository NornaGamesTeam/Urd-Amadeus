using System.Collections.Generic;
using MyBox;
using UnityEngine;
using Urd.Game;

namespace Urd.World
{
    public class WorldAreaView : MonoBehaviour
    {
        [field: SerializeField]
        public WorldAreaTypes WorldAreaType { get; private set; }

        [field: SerializeField]
        private Transform _initialPoint;
        
        [field: SerializeField, ReadOnly()]
        private GameWorldModel _gameWorldModel;

        private List<EnterArea> _enterAreas = new List<EnterArea>();

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            var enterAreas = GetComponentsInChildren<EnterArea>();
            if (enterAreas != null && enterAreas.Length > 0)
            {
                _enterAreas.AddRange(enterAreas);
            }

            for (int i = 0; i < _enterAreas.Count; i++)
            {
                _enterAreas[i].Init();
            }
        }

        public void SetModel(GameWorldModel gameWorldModel)
        {
            _gameWorldModel = gameWorldModel;
        }
    }
}