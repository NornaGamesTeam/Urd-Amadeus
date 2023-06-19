using UnityEngine;
using Urd.UI;

namespace Urd.Character
{
    public class CharacterModelListener : MonoBehaviour, IUICharacter
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