using System;
using UnityEngine;
using Urd.Services;
using Urd.Services.Physics;
using Urd.Utils;

namespace Urd.Character.Skill
{
    [Serializable]
    public class InteractSkillController : SkillController<InteractSkillModel>
    {
        private ServiceHelper<IPhysicsService> _physicsService = new ServiceHelper<IPhysicsService>();

        private IHitModel _lastInteractModel;
        
        public override void Init(ICharacterModel characterModel, ICharacterInput characterInput)
        {
            base.Init(characterModel, characterInput);

            SetModel(_characterModel.SkillSetModel.GetSkillModel<InteractSkillModel>());

            (_characterInput as MainCharacterInput).OnInteractChanged += OnSkillStatusChanged;
            _characterInput.OnAimDirectionChanged += OnAimChanged;
        }

        private void OnAimChanged(Vector2 aimDirection)
        {
            if(TryInteract(aimDirection, out IHitModel hitModel))
            {
                ShowInteractButton(hitModel, true);
            }
            else if(_lastInteractModel != null)
            {
                ShowInteractButton(_lastInteractModel, false);
                _lastInteractModel = null;
            }
        }
        

        public override void Dispose()
        {
            (_characterInput as MainCharacterInput).OnInteractChanged -= OnSkillStatusChanged;
            base.Dispose();
        }

        protected override void BeginSkill(Vector2 direction)
        {
            base.BeginSkill(direction);
            if (TryInteract(direction, out IHitModel hitModel))
            {
                Interact(hitModel);
            }
        }

        private bool TryInteract(Vector2 direction, out IHitModel hitModel)
        {
            var position = _characterModel.MovementModel.PhysicPosition + direction *_skillModel.Distance;

            hitModel = new InteractNPCModel(position, direction, _skillModel.AreaSphere);
            if (_physicsService.Service.TryHit(ref hitModel))
            {
                return true;
            }

            return false;
        }

        private void ShowInteractButton(IHitModel hitModel, bool showInteractButton)
        {
            // do this better
            var interactableObject = hitModel.Collisions[0].GetComponentInParent<IInteractable>();
            interactableObject.ShowInteractButton(showInteractButton);
            _lastInteractModel = hitModel;
        }

        private void Interact(IHitModel hitModel)
        {
            // do this better
            var interactableObject = hitModel.Collisions[0].GetComponentInParent<IInteractable>();
            var direction = hitModel.Collisions[0].transform.position -
                            (Vector3)_characterModel.MovementModel.PhysicPosition;
            interactableObject.Interact(-direction.normalized);
        }
    }
}