using System;
using System.Reflection;
using Urd.Utils;

namespace Urd.Services
{
    public abstract class BaseService : IBaseService
    {
        public bool InitBegins { get; protected set; }
        public bool IsLoaded { get; protected set; }
        public event Action OnFinishLoad;
        
        public IServiceLocator ServiceLocatorService { get; private set; }

        public virtual void Init()
        {
            InitBegins = true;
        }

        public Type GetMainInterface()
        {
            var interfaces = GetType().GetInterfaces();
            Type iBaseServiceType = typeof(IBaseService);
            for (int i = 0; i < interfaces.Length; i++)
            {
                if (iBaseServiceType.IsAssignableFrom(interfaces[i]) && iBaseServiceType != interfaces[i])
                {
                    return interfaces[i];
                }
            }

            return null;
        }

        public bool CanBeInitialized()
        {
            var fields = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            if (fields.Length <= 0)
            {
                return true;
            }
            
            Type iBaseServiceType = typeof(IBaseService);
            for (int i = 0; i < fields.Length; i++)
            {
                var type = fields[i].FieldType;
                if (iBaseServiceType.IsAssignableFrom(type) && (!StaticServiceLocator.Exist(type) || !StaticServiceLocator.Get(type).IsLoaded) )
                {
                    return false;
                }
            }
            
            return true;
        }
        
        protected void SetAsLoaded()
        {
            if (IsLoaded)
            {
                return;
            }
            
            IsLoaded = true;
            OnFinishLoad?.Invoke();
        }
        
        public void SetServiceLocatorService(IServiceLocator serviceLocatorService)
        {
            ServiceLocatorService = serviceLocatorService;
        }

    }
}