using NUnit.Framework;
using Urd.Pool;
using Urd.Services;

namespace Urd.Test
{
    public class TestPoolService
    {
        private IPoolService _poolService;

        [SetUp]
        public void SetUp()
        {
            _poolService = new PoolService();
            _poolService.Init();
        }

        [Test]
        public void PoolService_PreLoadClass_Success()
        {
            _poolService.PreLoadClassObject<DummyClass01>(2);

            Assert.That(true, Is.EqualTo(true));
        }
        
        [Test]
        public void PoolService_GetClass_Success()
        {
            _poolService.PreLoadClassObject<DummyClass01>(2);
            
            var dummyClass01 = _poolService.GetClassObject<DummyClass01>();

            Assert.That(dummyClass01, Is.Not.Null);
        }
        
        [Test]
        public void PoolService_GetClass_SuccessPreloadOnDemand()
        {
            _poolService.PreLoadClassObject<DummyClass01>(2);
            
            var dummyClass02 = _poolService.GetClassObject<DummyClass02>();

            Assert.That(dummyClass02?.InitCalled, Is.True);
        }
        
        [Test]
        public void PoolService_GetClass_SuccessForceCreation()
        {
            _poolService.PreLoadClassObject<DummyClass01>(1);
            
            var dummyClass01 = _poolService.GetClassObject<DummyClass01>();
            dummyClass01 = _poolService.GetClassObject<DummyClass01>();

            Assert.That(dummyClass01?.InitCalled, Is.True);
        }
        
        [Test]
        public void PoolService_FreeClass_Success()
        {
            _poolService.PreLoadClassObject<DummyClass01>(1);
            var dummyClass01 = _poolService.GetClassObject<DummyClass01>();

            _poolService.FreeClassObject(dummyClass01);

            Assert.That(dummyClass01?.DisposeCalled, Is.True);
        }

        private class DummyClass01 : IPoolable
        {
            public bool InitCalled { get; private set; }
            public bool DisposeCalled { get; private set; }

            public void Dispose()
            {
                DisposeCalled = true; }

            public void Init()
            {
                InitCalled = true;
            }
        }
        private class DummyClass02 : IPoolable
        {
            public bool InitCalled { get; private set; }
            public bool DisposeCalled { get; private set; }

            public void Dispose()
            {
                DisposeCalled = true; }

            public void Init()
            {
                InitCalled = true;
            }
        }
    }
}