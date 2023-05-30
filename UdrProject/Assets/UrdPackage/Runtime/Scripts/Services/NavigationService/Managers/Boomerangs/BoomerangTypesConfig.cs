using System.Collections.Generic;
using UnityEngine;
using Urd.Boomerang;
using Urd.View.Boomerang;

namespace Urd.Services.Navigation
{
    public class BoomerangTypesConfig : ScriptableObject
    {
        [field: SerializeField] public Canvas BoomerangCanvas { get; private set; }

        [field: SerializeField] public float BoomerangDefaultDuration { get; private set; }

        [field: SerializeField] public BoomerangBodyView BoomerangBodyPrefab { get; private set; }

        [SerializeField] private List<BoomerangConfig> _boomerangConfigs = new List<BoomerangConfig>();

        public bool Contains(INavigable navigable)
        {
            var boomerangModel = navigable as BoomerangModel;
            if (boomerangModel == null)
            {
                return false;
            }

            var boomerangType = boomerangModel.BoomerangType;
            return _boomerangConfigs.Exists(
                boomerangInfo => boomerangInfo.BoomerangModel.BoomerangType == boomerangType);
        }

        public bool TryGetBoomerangModel(BoomerangTypes boomerangType, out BoomerangModel boomerangModel)
        {
            var boomerangConfig = _boomerangConfigs
                                   .Find(boomerangInfo =>
                                             boomerangInfo.BoomerangModel.BoomerangType == boomerangType);
            boomerangModel = boomerangConfig?.BoomerangModel;
            return boomerangModel != null;
        }

        public bool TryGetBoomerangView(BoomerangModel navigable, out IBoomerangView boomerangView)
        {
            var boomerangConfig = _boomerangConfigs
                                   .Find(boomerangInfo =>
                                             boomerangInfo.BoomerangModel.BoomerangType == navigable.BoomerangType);
            boomerangView = boomerangConfig.BoomerangView as IBoomerangView;
            return boomerangView != null;
        }
    }
}