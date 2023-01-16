namespace Urd.Services
{
    public class StartUpService : BaseService, IStartUpService
    {
        public override void Init()
        {
            base.Init();

            ServiceLocatorService.Register<ICoroutineService>(new CoroutineService());
            ServiceLocatorService.Register<IClockService>(new ClockService());
            ServiceLocatorService.Register<IAssetService>(new AssetService());
            
            ServiceLocatorService.Register<INavigationService>(new NavigationService());
        }
    }
}