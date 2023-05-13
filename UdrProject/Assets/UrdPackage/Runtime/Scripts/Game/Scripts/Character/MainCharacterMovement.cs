using Urd.Services;
using Urd.Utils;

namespace Urd.Character
{
    public class MainCharacterMovement
    {
        private MainCharacterInput _mainCharacterInput;

        private CharacterModel _characterModel;
        
        public MainCharacterMovement(CharacterModel characterModel)
        {
            _characterModel = characterModel;
            
            Init();
        }

        public void Init()
        {
            _mainCharacterInput = new MainCharacterInput(_characterModel);
            
            SubscribeToUpdate();
        }

        private void SubscribeToUpdate()
        {
            StaticServiceLocator.Get<IClockService>().SubscribeToUpdate(OnUpdate);
        }

        private void OnUpdate(float deltaTime)
        {
            if (_mainCharacterInput.Movement.sqrMagnitude > 0)
            {
                _characterModel.CharacterMovement.SetRawNormalizedMovement(_mainCharacterInput.Movement);
                _characterModel.CharacterMovement.ModifyPosition(_mainCharacterInput.Movement * _characterModel.Speed * deltaTime);
                _characterModel.CharacterMovement.SetIsMoving(true);
            }
            else
            {
                _characterModel.CharacterMovement.SetIsMoving(false);
            }
        }
    }
}