using MyBox;
using UnityEngine;
using Urd.Game;

namespace Urd.World
{
    public class WorldAreaView : MonoBehaviour
    {
        [field: SerializeField]
        public WorldAreaTypes WorldAreaType { get; private set; }

        [field: SerializeField, ReadOnly()]
        private GameWorldModel _gameWorldModel;
        
        public void SetModel(GameWorldModel gameWorldModel)
        {
            _gameWorldModel = gameWorldModel;
        }
    }
}