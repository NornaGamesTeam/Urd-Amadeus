using UnityEngine;

namespace Urd.DebugTools
{
    public abstract class DebugAbstract : MonoBehaviour
    {
        [SerializeField] private bool _enabled;

        [SerializeField] private KeyCode _keyCode = KeyCode.Alpha1;

        void Update()
        {
            if (!_enabled)
            {
                return;
            }

            if (Input.GetKeyDown(_keyCode))
            {
                OnInputGetDown();
            }
        }

        public abstract void OnInputGetDown();
    }
}