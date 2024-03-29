using System;
using System.Collections.Generic;
using UnityEngine;
using Urd.Game.SkillTrees;
using Urd.Inputs;
using Urd.Services;
using Urd.Services.Physics;
using Urd.Utils;

namespace Urd.Character.Skill
{
    [Serializable] 
    public class MeleeAttackController : SkillController<MeleeAttackModel>
    {
        private List<AttackAreaModel> _hitAreas;

        private ServiceHelper<IPhysicsService> _physicsService = new ServiceHelper<IPhysicsService>();

        private List<Collider2D> _alreadyHit = new ();
        
        public override void Init(ISkillModel skillModel, ICharacterModel characterModel,
            ICharacterInput characterInput)
        {
            base.Init(skillModel, characterModel, characterInput);

            SetModel(_characterModel.SkillSetModel.MeleeAttackModel);
            
            _characterInput.OnAttackingChanged += OnSkillStatusChanged;
        }

        public override void Dispose()
        {
            _characterInput.OnAttackingChanged -= OnSkillStatusChanged;
            base.Dispose();
        }

        private void OnSkillStatusChanged(bool isAttacking, Vector2 attackDirection, SkillActionType skillActionType)
        {
            if (skillActionType == SkillActionType.Melee)
            {
                OnSkillStatusChanged(isAttacking, attackDirection);
            }
        }
        
        protected override void BeginSkill(Vector2 direction)
        {
            base.BeginSkill(direction);
            
            _alreadyHit.Clear();
            var skillDirection = direction.ConvertToDirection();
            _direction = skillDirection.ConvertToVector2();
            _hitAreas = _skillModel.DamageOverTime.Find( hitArea => hitArea.Direction == skillDirection)?.HitArea;
        }

        protected override void SkillUpdate(float deltaTime)
        {
            base.SkillUpdate(deltaTime);

            CheckDamage();
        }
        
        private void CheckDamage()
        {
            var hitAreasActives = GetAreasToCheck();
            for (int i = 0; i < hitAreasActives.Count; i++)
            {
                var direction = _direction;
                if (hitAreasActives[i].RotationDegreesClockWise != 0)
                {
                    direction = _direction.RotateDegrees(hitAreasActives[i].RotationDegreesClockWise);
                    
                }
                var position = _characterModel.MovementModel.PhysicPosition + hitAreasActives[i].OriginPoinOffset;

                IHitModel hitModel = new HitEnemyModel(position, direction, hitAreasActives[i].AreaShapeModel);
                if (_physicsService.Service.TryHit(ref hitModel))
                {
                    CheckHit(hitAreasActives[i], hitModel);
                }
            }
        }

        private void CheckHit(AttackAreaModel attackAreaModel, IHitModel hitModel)
        {
            for (int i = 0; i < hitModel.Collisions.Count; i++)
            {
                if (!_alreadyHit.Contains(hitModel.Collisions[i]))
                {
                    Hit(attackAreaModel, hitModel.Collisions[i]);
                    
                    _alreadyHit.Add(hitModel.Collisions[i]);
                }
            }
        }

        private void Hit(AttackAreaModel attackAreaModel, Collider2D collider)
        {
            // do this better
            var hittableObject = collider.GetComponentInParent<IHittable>();
            float damageFactor = _skillModel.DamageFromStats * attackAreaModel.DamagePercentage;
            float damage = _characterModel.CharacterStatsModel.GetFinalDamage(_skillModel.DamageElement, damageFactor);
            var attackDirection = collider.transform.position - (Vector3)_characterModel.MovementModel.PhysicPosition;
            hittableObject.Hit(damage, attackDirection.normalized);
        }

        private List<AttackAreaModel> GetAreasToCheck()
        {
            return _hitAreas.FindAll(
                damageOverTime => damageOverTime.BeginTime < _skillTime
                                  && _skillTime < damageOverTime.EndTime);
            
        }
    }
}