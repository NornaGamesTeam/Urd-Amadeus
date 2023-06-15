using MyBox;
using UnityEngine;

namespace Urd.Services.Physics
{
    public class DebugAreaShapeView : MonoBehaviour
    {
        [SerializeField] 
        private bool _drawGizmos = true;
        
        [field: SerializeReference]
        public IAreaShapeModel AreaShapeModel  { get; private set; }
        [field: SerializeField] 
        public Vector2 OriginPoint { get; private set; }
        [field: SerializeField]
        public Vector2 Direction { get; private set; }

        [SerializeField, PositiveValueOnly] private int _steps = 20;

        public void SetAreaShapeModel(Vector2 originPoint, Vector2 direction, IAreaShapeModel areaShapeModel)
        {
            AreaShapeModel = areaShapeModel;
            OriginPoint = originPoint;
            Direction = direction;
        }

        public void DestroyAt(float timeToDestroy)
        {
            GameObject.Destroy(gameObject, timeToDestroy);
        }

        private void OnDrawGizmos()
        {
            if (AreaShapeModel == null || !_drawGizmos)
            {
                return;
            }

            switch (AreaShapeModel)
            {
                case AreaShapeConeModel:
                    DrawCone();
                    break;
                case AreaShapeBoxModel:
                    DrawBox();
                    break;
                case AreaShapeSphereModel:
                    DrawSphere();
                    break;
            }
        }

        private void DrawSphere()
        {
            var areaShapeSphereModel = AreaShapeModel as AreaShapeSphereModel;
            
            Gizmos.DrawSphere(OriginPoint, areaShapeSphereModel.Radio);
        }

        private void DrawCone()
        {
            var areaShapeConeModel = AreaShapeModel as AreaShapeConeModel;
            
            float angle = areaShapeConeModel.AngleDegreesClockWise;
            float rayRange = areaShapeConeModel.Distance;

            var initialRadians = Mathf.Deg2Rad * angle* 0.5f;
           
            Gizmos.color = Color.white;
            Gizmos.DrawRay(OriginPoint, Direction*rayRange);

            float anglePerStep = angle / _steps;
            
            for (int i = 0; i <= _steps; i++)
            {
                anglePerStep = Mathf.Lerp(-angle*0.5f, angle*0.5f,(i)/(float)_steps);

                var direction = Quaternion.AngleAxis(anglePerStep, Vector3.forward) * Direction;
                Gizmos.DrawRay(OriginPoint, direction*rayRange);
            }
        }

        private void DrawBox()
        {
            var areaShapeBoxModel = AreaShapeModel as AreaShapeBoxModel;

            transform.position = OriginPoint;
            transform.LookAt(OriginPoint+Direction, Vector3.forward);

            var boxArea = areaShapeBoxModel.Area;
            
            for (int i = 0; i <= _steps; i++)
            {
                Vector3 position = Vector3.Lerp(
                    transform.position-(transform.right * boxArea.x * 0.5f), 
                    transform.position+transform.right * boxArea.x * 0.5f,
                                                (i) / (float)_steps);
                Gizmos.DrawRay(position, transform.forward*boxArea.y);
            }
        }
    }
}
