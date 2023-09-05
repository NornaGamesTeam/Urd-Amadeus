using System;
using UnityEngine;

namespace Urd.Utils.Game.Physics
{
    public class OnTriggerDetection : MonoBehaviour, IOnTriggerDetection
    {
        [SerializeField]
        public LayerMaskTypes Detection { get; private set; }
        
        public event Action<Collider2D> OnTriggerEnter; 
        public event Action<Collider2D> OnTriggerStay; 
        public event Action<Collider2D> OnTriggerExit;

        public void SetDetectionLayers(LayerMaskTypes detection)
        {
            Detection = detection;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            OnTriggerEnter?.Invoke(other);
        }
        
        private void OnTriggerStay2D(Collider2D other)
        {
            OnTriggerStay?.Invoke(other);
        }
        
        private void OnTriggerExit2(Collider2D other)
        {
            OnTriggerExit?.Invoke(other);
        }
    }
}
