using System.Collections.Generic;
using MyBox;
using UnityEngine;
using Urd.World;

namespace Urd.GameManager
{
    public class WorldManagerConfig : ScriptableObject
    {
        [SerializeField, DisplayInspector()]
        private List<WorldAreaView> _worldAreas;

        public bool TryGetArea(WorldAreaTypes worldAreaType, out WorldAreaView worldAreaView)
        {
            var areaView = _worldAreas.Find(worldArea => worldArea.WorldAreaType == worldAreaType);
            worldAreaView = areaView;
            return worldAreaView != null;
        }
    }
}