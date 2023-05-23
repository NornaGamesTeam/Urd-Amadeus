using System.Collections.Generic;
using UnityEngine;
using Urd.Error;
using Urd.Services.Physics;

namespace Urd.Services
{
    [System.Serializable]
    public class PhysicsService : BaseService, IPhysicsService
    {
        [SerializeReference, SubclassSelector]
        private List<IPhysicsAreaShapeManager> _shapeManager = new ();
        public override void Init()
        {
            base.Init();
            
            SetAsLoaded();
        }

        public bool TryHit(Vector2 originPoint, Vector2 direction, IAreaShapeModel areaShapeModel)
        {
            var manager = GetManager(areaShapeModel);
            if (manager == null)
            {
                var error = new ErrorModel($"Manager for shape: {areaShapeModel.AreaShape} Not Found",
                                           ErrorCode.Error_404_Not_Found);
                Debug.LogWarning(error);
                return false;
            }

            return manager.TryHit(originPoint, direction, areaShapeModel);
        }

        private IPhysicsAreaShapeManager GetManager(IAreaShapeModel areaShapeModel)
        {
            return _shapeManager.Find(manager => manager.AreaShape == areaShapeModel.AreaShape);
        }
    }
}