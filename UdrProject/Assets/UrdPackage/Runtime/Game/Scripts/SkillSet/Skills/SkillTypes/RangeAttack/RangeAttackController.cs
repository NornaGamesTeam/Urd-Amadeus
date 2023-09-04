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
    public class RangeAttackController : SkillController<RangeAttackModel>
    {
        private List<AttackAreaModel> _hitAreas;

        private ServiceHelper<IPhysicsService> _physicsService = new ServiceHelper<IPhysicsService>();

        private List<Collider2D> _alreadyHit = new ();
        
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
            
           // StaticServiceLocator.Get<IPoolService>().PreLoadGameObject(Mode);
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
           Debug.Log("TTT Range Attack");
           StaticServiceLocator.Get<IPoolService>()
        }
    }
}