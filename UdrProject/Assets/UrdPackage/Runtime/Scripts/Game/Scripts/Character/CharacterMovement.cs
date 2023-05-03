using Urd.Services;
using Urd.Utils;

namespace Urd.Character
{
    public class CharacterMovement
    {
        private CharacterInput _characterInput;

        private CharacterModel _characterModel;
        
        public CharacterMovement(CharacterModel characterModel)
        {
            _characterModel = characterModel;
            
            Init();
        }

        public void Init()
        {
            _characterInput = new CharacterInput(_characterModel);
            
            SubscribeToUpdate();
        }

        private void SubscribeToUpdate()
        {
            StaticServiceLocator.Get<IClockService>().SubscribeToUpdate(OnUpdate);
        }

        private void OnUpdate(float deltaTime)
        {
            _characterModel.ModifyPosition(_characterInput.Movement * _characterModel.Speed * deltaTime);
        }
    }
}