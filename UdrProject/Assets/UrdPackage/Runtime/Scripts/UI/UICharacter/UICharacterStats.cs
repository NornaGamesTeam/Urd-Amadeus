using TMPro;
using UnityEngine;
using Urd.Character;
using CharacterController = Urd.Character.CharacterController;

namespace Urd.UI
{
    public class UICharacterStats : MonoBehaviour
    {
        [SerializeField] 
        private CharacterController _characterController;
        
        void Start()
        {
            SetModels();

            
}

        private void SetModels()
        {
            foreach (var uiCharacterComponent in GetComponentsInChildren<IUICharacter>())
            {
                uiCharacterComponent.SetCharacterModel(_characterController.CharacterModel);
            }
            
        }
    }
}
