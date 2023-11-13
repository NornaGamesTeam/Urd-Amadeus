using System.Collections.Generic;
using UnityEngine;
using Urd.View.WorldAreaTransition;
using Urd.WorldAreaTransition;

namespace Urd.Services.Navigation
{
    public class WorldAreaTransitionTypesConfig : ScriptableObject
    {
        [field: SerializeField]
        public Canvas WorldAreaCanvas { get; private set; }
        
        [SerializeField]
        private List<WorldAreaTransitionTypesConfigInfo> _worldAreaTransitionList = new List<WorldAreaTransitionTypesConfigInfo>();

        public bool Contains(INavigable navigable)
        {
            var worldAreaTransitionModel = navigable as WorldAreaTransitionModel;
            if (worldAreaTransitionModel == null)
            {
                return false;
            }
            
            var worldAreaTransitionType = worldAreaTransitionModel.WorldAreaTransitionType;
            return _worldAreaTransitionList.Exists( configInfo => configInfo.WorldAreaTransitionType == worldAreaTransitionType);
        }
        
        public bool TryGetWorldAreaTransitionView(WorldAreaTransitionModel navigable, out IWorldAreaTransitionView worldAreaTransitionView)
        {
            var rawPopupView = _worldAreaTransitionList.Find( 
                configInfo => configInfo.WorldAreaTransitionType == navigable.WorldAreaTransitionType)?.WorldAreaTransitionViewNoModel;
            worldAreaTransitionView = rawPopupView as IWorldAreaTransitionView;
            return worldAreaTransitionView != null;
        }
    }

    [System.Serializable]
    internal class WorldAreaTransitionTypesConfigInfo
    {
        [field: SerializeField]
        public WorldAreaTransitionTypes WorldAreaTransitionType { get; private set; }
        [field: SerializeField]
        public WorldAreaTransitionViewNoModel WorldAreaTransitionViewNoModel { get; private set; }
    }
}