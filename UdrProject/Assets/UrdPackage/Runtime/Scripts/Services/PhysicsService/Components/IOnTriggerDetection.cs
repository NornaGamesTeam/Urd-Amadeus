using System;
using UnityEngine;

namespace Urd.Utils.Game.Physics
{
    public interface IOnTriggerDetection
    {
        LayerMaskTypes Detection { get; }
        event Action<Collider2D> OnTriggerEnter;
        event Action<Collider2D> OnTriggerStay;
        event Action<Collider2D> OnTriggerExit;
        void SetDetectionLayers(LayerMaskTypes detection);
    }
}