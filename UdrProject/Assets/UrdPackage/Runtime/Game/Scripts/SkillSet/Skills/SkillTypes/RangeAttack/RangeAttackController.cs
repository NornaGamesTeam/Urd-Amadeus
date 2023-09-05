using System;
using System.Collections.Generic;
using UnityEngine;
using Urd.Game.Projectile;
using Urd.Inputs;
using Urd.Services;
using Urd.Utils;

namespace Urd.Character.Skill
{
    [Serializable] 
    public class RangeAttackController : SkillController<RangeAttackModel>
    {
        private ServiceHelper<IPoolService> _poolService = new ServiceHelper<IPoolService>();
        private ServiceHelper<IClockService> _clockService = new ServiceHelper<IClockService>();

        private List<IProjectileModel> _projectiles = new List<IProjectileModel>();
        public override void Init(ISkillModel skillModel, ICharacterModel characterModel,
            ICharacterInput characterInput)
        {
            base.Init(skillModel, characterModel, characterInput);

            SetModel(_characterModel.SkillSetModel.GetSkillModel<RangeAttackModel>());
            
            _characterInput.OnAttackingChanged += OnSkillStatusChanged;

            _clockService.Service.SubscribeToUpdate(CustomUpdate);

            InitProjectile();
        }

        private void InitProjectile()
        {
            var projectile = _skillModel.ProjectileConfig.ProjectileModel.ProjectileView;
            _poolService.Service.PreLoadGameObject(
                projectile.gameObject, 
                projectile.name,
                1);
        }

        private void OnSkillStatusChanged(bool isAttacking, Vector2 attackDirection, SkillActionType skillActionType)
        {
            if (skillActionType == SkillActionType.Range)
            {
                OnSkillStatusChanged(isAttacking, attackDirection);
            }
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

            var projectileModel = _skillModel.ProjectileConfig.ProjectileModel;
            if (projectileModel.HasDelayProjectile)
            {
                _clockService.Service.AddDelayCall(projectileModel.DelayProjectile,
                                                   () => SpawnProjectile(direction));
            }
            else
            {
                SpawnProjectile(direction);
            }
        }

        private void SpawnProjectile(Vector2 direction)
        {
            var projectileModel = new ProjectileModel(_skillModel.ProjectileConfig.ProjectileModel);
            var projectileGameObject = _poolService.Service.GetGameObject(projectileModel.ProjectileView.name);

            projectileGameObject.transform.parent = null;
            projectileGameObject.transform.localScale = Vector3.one;

            var projectileView = projectileGameObject.GetComponent<ProjectileView>();
            projectileView.SetUp(projectileModel);
            projectileView.Begin(OnCollision);

            projectileModel.SetInitialPositionAndDirection(_characterModel.MovementModel.Position,
                                                           direction);
            _projectiles.Add(projectileModel);
        }

        private void CustomUpdate(float deltaTime)
        {
            for (int i = 0; i < _projectiles.Count; i++)
            {
                var projectile = _projectiles[i];
                Vector3 movement = (projectile.Direction * projectile.Speed * deltaTime);
                projectile.Move(movement); 
            }
        }
        
        private void OnCollision()
        {
            
        }
    }
}