
using System;
using MyBox;
using UnityEngine;

namespace Urd.Services.Physics
{
    public class DebugAreaShapeView : MonoBehaviour
    {
        [SerializeReference]
        private IAreaShapeModel _areaShapeModel;
        [SerializeField]
        private Vector3 _originPoint;
        [SerializeField]
        private Vector2 _direction;

        [SerializeField, PositiveValueOnly] private int _steps = 100;

        public void SetAreaShapeModel(Vector2 originPoint, Vector2 direction, IAreaShapeModel areaShapeModel)
        {
            _areaShapeModel = areaShapeModel;
            _originPoint = originPoint;
            _direction = direction;
        }

        public void DestroyAt(float timeToDestroy)
        {
            GameObject.Destroy(gameObject, timeToDestroy);
        }

        private void OnDrawGizmos()
        {
            if (_areaShapeModel == null)
            {
                return;
            }

            switch (_areaShapeModel)
            {
                case AreaShapeConeModel:
                    DrawCone();
                    break;
                case AreaShapeBoxModel:
                    DrawBox();
                    break;
            }
        }

        private void DrawCone()
        {
            var areaShapeConeModel = _areaShapeModel as AreaShapeConeModel;
            
            float angle = areaShapeConeModel.AngleDegreesClockWise;
            float rayRange = areaShapeConeModel.Distance;

            var initialRadians = Mathf.Deg2Rad * angle* 0.5f;
           
            Gizmos.color = Color.white;
            Gizmos.DrawRay(_originPoint, _direction*rayRange);

            Vector3 direction = Quaternion.AngleAxis(-angle * 0.5f, Vector3.forward) * _direction;
            float anglePerStep = angle / _steps;
            
            for (int i = 0; i < _steps; i++)
            {
                Gizmos.DrawRay(_originPoint, direction*rayRange);
                direction = Quaternion.AngleAxis(anglePerStep, Vector3.forward) * direction;
            }
        }

        private void DrawBox()
        {

        }

    }
}
