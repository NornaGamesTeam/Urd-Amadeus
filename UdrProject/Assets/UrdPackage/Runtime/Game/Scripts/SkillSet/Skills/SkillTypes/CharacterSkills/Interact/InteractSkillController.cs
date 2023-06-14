using System;
using System.Collections.Generic;
using UnityEngine;
using Urd.Game.SkillTrees;
using Urd.Services;
using Urd.Services.Physics;
using Urd.Utils;

namespace Urd.Character.Skill
{
    [Serializable] 
    public class InteractSkillController : SkillController<InteractSkillModel>
    {
        private ServiceHelper<IPhysicsService> _physicsService = new ServiceHelper<IPhysicsService>();
        
        public override void Init(ICharacterModel characterModel, ICharacterInput characterInput)
        {
            base.Init(characterModel, characterInput);

            SetModel(_characterModel.SkillSetModel.GetSkillModel<InteractSkillModel>());
            
            _characterInput.OnInteractDelegate += OnSkillStatusChanged;
        }
        
        public override void Dispose()
        {
            _characterInput.OnAttackingChanged -= OnSkillStatusChanged;
            base.Dispose();
        }

        protected override void BeginSkill(Vector2 direction)
        {
            base.BeginSkill(direction);
            
            var skillDirection = direction.ConvertToDirection();
            _direction = skillDirection.ConvertToVector2();
            CheckInteraction();
        }

        private void CheckInteraction()
        {
                var direction = _direction;
                var position = _characterModel.CharacterMovement.Position;

                IHitModel hitModel = new HitEnemyModel(position, direction, _skillModel.Sphere);
                if (_physicsService.Service.TryHit(ref hitModel))
                {
                    Interact(hitModel);
                }
        }
        
        private void Interact(IHitModel hitModel)
        {
            for (int i = 0; i < hitModel.Collisions.Count; i++)
            {
                // do this better
                var interactableObject = hitModel.Collisions[i].GetComponentInParent<IInteractable>();
                var direction = hitModel.Collisions[i].transform.position - (Vector3)_characterModel.CharacterMovement.Position;
                interactableObject.Interact(direction.normalized);
            }
        }
    }
}