namespace Urd.Services
{
    [System.Serializable]
    public class PhysicsService : BaseService, IPhysicsService
    {
        public override void Init()
        {
            base.Init();
            
            SetAsLoaded();
        }
    }
}