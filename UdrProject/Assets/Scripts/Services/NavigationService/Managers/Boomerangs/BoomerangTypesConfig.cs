using System.Collections.Generic;
using UnityEngine;
using Urd.Boomerang;
using Urd.View.Boomerang;

namespace Urd.Services.Navigation
{
    public class BoomerangTypesConfig : ScriptableObject
    {
        [field: SerializeField]
        public float BoomerangDefaultDuration { get; private set; }
        
        [field: SerializeField]
        public BoomerangBodyView BoomerangBodyPrefab { get; private set; }

        [SerializeField]
        private List<BoomerangTypesConfigInfo> _boomerangList = new List<BoomerangTypesConfigInfo>();

        public bool Contains(INavigable navigable)
        {
            var boomerangModel = navigable as BoomerangModel;
            if (boomerangModel == null)
            {
                return false;
            }
            
            var boomerangType = boomerangModel.BoomerangType;
            return _boomerangList.Exists( boomerangInfo => boomerangInfo.BoomerangType == boomerangType);
        }
        
        public bool TryGetBoomerangView(BoomerangModel navigable, out IBoomerangView boomerangView)
        {
            var rawBoomerangView = _boomerangList.Find( boomerangInfo => boomerangInfo.BoomerangType == navigable.BoomerangType)?.BoomerangView;
            boomerangView = rawBoomerangView as IBoomerangView;
            return boomerangView != null;
        }
    }

    [System.Serializable]
    internal class BoomerangTypesConfigInfo
    {
        [field: SerializeField]
        public BoomerangTypes BoomerangType { get; private set; }
        [field: SerializeField]
        public BoomerangViewNoModel BoomerangView { get; private set; }
    }
}