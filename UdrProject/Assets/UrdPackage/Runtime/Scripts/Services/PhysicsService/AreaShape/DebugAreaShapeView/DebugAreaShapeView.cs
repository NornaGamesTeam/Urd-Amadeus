
using System;
using UnityEngine;

namespace Urd.Services.Physics
{
    public class DebugAreaShapeView : MonoBehaviour
    {
        private IAreaShapeModel _areaShapeModel;
        private Vector3 _originPoint;
        private Vector2 _direction;

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
            float halfFOV = angle / 2.0f;
            float coneDirection = 180;

            Quaternion upRayRotation = Quaternion.AngleAxis(-halfFOV, _direction);
            Quaternion downRayRotation = Quaternion.AngleAxis(halfFOV, _direction);

            Vector3 upRayDirection = upRayRotation * transform.right * rayRange;
            Vector3 downRayDirection = downRayRotation * transform.right * rayRange;

            Gizmos.DrawRay(_originPoint, upRayDirection);
            Gizmos.DrawRay(_originPoint, downRayDirection);
            Gizmos.DrawLine(_originPoint + downRayDirection, _originPoint + upRayDirection);
        }

        private void DrawBox()
        {

        }

    }
}
