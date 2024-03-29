using System;
using System.Collections.Generic;
using UnityEngine;
using Urd.Game.Projectile;
using Urd.Inputs;
using Urd.Services;
using Urd.Services.Physics;
using Urd.Utils;

namespace Urd.Character.Skill
{
    [Serializable] 
    public class RangeAttackController : SkillController<RangeAttackModel>
    {
        private ServiceHelper<IPoolService> _poolService = new ServiceHelper<IPoolService>();
        private ServiceHelper<IClockService> _clockService = new ServiceHelper<IClockService>();
        private ServiceHelper<IPhysicsService> _physicService = new ServiceHelper<IPhysicsService>();

        private List<IProjectileModel> _projectiles = new List<IProjectileModel>();
        private Dictionary<IProjectileModel, ProjectileView> _projectilesViewsByModels = new ();
        
        private List<Collider2D> _alreadyHit = new ();

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
            var projectile = _skillModel.ProjectileConfig.ProjectileModel.ProjectileViewPrefab;
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
            
            _alreadyHit.Clear();

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
            var projectileGameObject = _poolService.Service.GetGameObject(projectileModel.ProjectileViewPrefab.name);
            projectileGameObject.gameObject.SetActive(true);
            projectileGameObject.transform.parent = null;
            projectileGameObject.transform.localScale = Vector3.one;

            var projectileView = projectileGameObject.GetComponent<ProjectileView>();
            projectileView.SetUp(projectileModel);

            projectileModel.SetInitialPositionAndDirection(_characterModel.MovementModel.Position,
                                                           direction);
            _projectiles.Add(projectileModel);
            _projectilesViewsByModels.Add(projectileModel, projectileGameObject.GetComponent<ProjectileView>());
        }

        private void CustomUpdate(float deltaTime)
        {
            for (int i = 0; i < _projectiles.Count; i++)
            {
                var projectile = _projectiles[i];
                Vector3 movement = (projectile.Direction * projectile.Speed * deltaTime);
                projectile.Move(movement);
                CheckCollision(projectile);
            }
        }

        private void CheckCollision(IProjectileModel projectileModel)
        {
            var directionType = DirectionUtils.ConvertToDirection(projectileModel.Direction);

            var areaShape = projectileModel.AreaShape?.Find(
                areaShapeModel => areaShapeModel.Direction == directionType)?.Item ?? new AreaShapeSphereModel();
            
            IHitModel hitModel = new HitModel(
                projectileModel.Position,
                projectileModel.Direction, 
                areaShape, 
                projectileModel.Objetive);
            hitModel.DrawDebug = true;
            if (_physicService.Service.TryHit(ref hitModel))
            {
                CheckHit(projectileModel, hitModel);
            }
        }
        
        private void CheckHit(IProjectileModel projectileModel, IHitModel hitModel)
        {
            for (int i = 0; i < hitModel.Collisions.Count; i++)
            {
                if (!_alreadyHit.Contains(hitModel.Collisions[i]))
                {
                    Hit(projectileModel, hitModel.Collisions[i]);
                    
                    _alreadyHit.Add(hitModel.Collisions[i]);
                    DestroyProjectile(projectileModel);
                    return;
                }
            }
        }

        private void DestroyProjectile(IProjectileModel projectileModel)
        {
            var projectileView = _projectilesViewsByModels[projectileModel];

            _projectilesViewsByModels.Remove(projectileModel);
            _projectiles.Remove(projectileModel);
            
            projectileModel.Dispose();
            _poolService.Service.FreeGameObject(projectileModel.ProjectileViewPrefab.name, projectileView.gameObject);
        }

        private void Hit(IProjectileModel projectileModel, Collider2D collider)
        {
            var hittableObject = collider.GetComponentInParent<IHittable>();
            if (hittableObject == null)
            {
                return;
            }
            
            float damage = _characterModel.CharacterStatsModel.GetFinalDamage(projectileModel.DamageElement, projectileModel.DamageFromStats);
            var attackDirection = collider.transform.position - (Vector3)projectileModel.Position;
            hittableObject.Hit(damage, attackDirection.normalized);
        }
    }
}