using UnityEngine;
using Urd.Services;

public class StartUpServiceView : MonoBehaviour
{
    void Awake()
    {
        var serviceLocator = new ServiceLocator();
        var startUpService = new StartUpService();

        serviceLocator.Register<IStartUpService>(startUpService);
    }
}
