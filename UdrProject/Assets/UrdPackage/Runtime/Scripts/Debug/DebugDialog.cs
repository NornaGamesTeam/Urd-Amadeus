using UnityEngine;
using Urd.Game;
using Urd.Utils;

namespace Urd.DebugTools
{
    public class DebugDialog : DebugAbstract
    {
        [SerializeField] 
        private string debugText = "esto es un test de prueba";
        public override void OnInputGetDown()
        {
            StaticServiceLocator.Get<IGameManagerService>().DialogModule.ShowDialog(debugText);
        }
    }
}