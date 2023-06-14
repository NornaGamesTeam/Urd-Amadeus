using UnityEngine;

namespace Urd.Character
{
    public class CharacterModelListener : MonoBehaviour
    {
        protected ICharacterModel _characterModel;

        public void SetCharacterModel(ICharacterModel characterModel)
        {
            _characterModel = characterModel;
            Init();
        }
        
        public virtual void Init(){}
    }
}