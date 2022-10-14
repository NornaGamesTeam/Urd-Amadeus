using NUnit.Framework;
using Urd.Services;

public class TestServiceLocator
{
    IServiceLocator _serviceLocator;

    [SetUp]
    public void SetUp()
    {
        _serviceLocator = new ServiceLocator();
    }

    [Test]
    public void ServiceLocator_GetService_Success()
    {
        var dummyService1 = new DummyService1()
        {
            BoolVariable = true
        };
        _serviceLocator.Register<IDummyService1>(dummyService1);

        var dummyServiceLoaded = _serviceLocator.Get<IDummyService1>();

        Assert.That(dummyServiceLoaded, Is.EqualTo(dummyService1));
        Assert.That(dummyServiceLoaded.BoolVariable, Is.EqualTo(dummyService1.BoolVariable));
    }

    [Test]
    public void ServiceLocator_GetService_FailedBecauseServiceDoesntExit()
    {
        var dummyService1 = new DummyService1()
        {
            BoolVariable = true
        };
        _serviceLocator.Register<IDummyService1>(dummyService1);

        var dummyServiceLoaded = _serviceLocator.Get<IDummyService2>();

        Assert.That(dummyServiceLoaded, Is.Null);
    }

    public class DummyService1 : BaseService, IDummyService1
    {
        public bool BoolVariable { get; set; }
    }
    public interface IDummyService1 : IBaseService
    {
        bool BoolVariable { get; set; }
    }

    public class DummyService2 : BaseService, IDummyService2
    {
        public int IntVariable { get; set; }
    }
    public interface IDummyService2 : IBaseService
    {
        int IntVariable { get; set; }
    }

}
