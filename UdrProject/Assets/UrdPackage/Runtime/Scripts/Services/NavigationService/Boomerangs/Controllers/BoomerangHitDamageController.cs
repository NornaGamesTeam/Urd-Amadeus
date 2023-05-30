using System;
using UnityEngine;

namespace Urd.Boomerang
{
    [Serializable]
    public class BoomerangHitDamageController: BoomerangController<BoomerangHitDamageModel>
    {
        public override void Open()
        {
            base.Open();

            InitAnim();
        }

        private void InitAnim()
        {
            BoomerangBody.transform.position = BoomerangModel.OriginPoint;
            _clockService.Service.SubscribeToUpdate(CustomUpdate);
        }

        private void CustomUpdate(float deltaTime)
        {
            BoomerangBody.transform.position += Vector3.up * deltaTime * BoomerangModel.Speed;
        }

        protected override void OnClose()
        {
            base.OnClose();
            _clockService.Service.UnSubscribeToUpdate(CustomUpdate);
        }
    }
}