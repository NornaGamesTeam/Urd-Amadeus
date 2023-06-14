using UnityEngine;
using Urd.Character;

namespace Urd.UI
{
    public class UICharacterStats : MonoBehaviour
    {
        [SerializeField] private CharacterControllerNoModel _characterController;

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
