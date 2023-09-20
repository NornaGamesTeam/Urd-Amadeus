using UnityEngine;

namespace Urd.GameManager
{
    public class GameManagerConfig : ScriptableObject
    {
        [field: Header("Save Load Data"), SerializeField]
        public int SaveLoadDataAmount { get; private set; }

    }
}
