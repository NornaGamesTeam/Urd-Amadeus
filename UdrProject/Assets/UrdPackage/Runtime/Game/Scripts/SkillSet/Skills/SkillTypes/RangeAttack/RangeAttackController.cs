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

        private List<ProjectileView> _projectileViews = new List<ProjectileView>();
        public override void Init(ISkillModel skillModel, ICharacterModel characterModel,
            ICharacterInput characterInput)
        {
            base.Init(skillModel, characterModel, characterInput);

            SetModel(_characterModel.SkillSetModel.GetSkillModel<RangeAttackModel>());
            
            _characterInput.OnAttackingChanged += OnSkillStatusChanged;

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

            SpawnProjectile();
        }

        private void SpawnProjectile()
        {
            var projectileGameObject = _poolService.Service.
                                                  GetGameObject(_skillModel.ProjectileConfig.ProjectileModel.ProjectileView.name);

            var projectileView = projectileGameObject.GetComponent<ProjectileView>();
            projectileView.SetUp(_skillModel.ProjectileConfig.ProjectileModel);
            projectileView.Begin(OnCollision);

        }

        private void OnCollision()
        {
            
        }
    }
}