using NSubstitute;
using NUnit.Framework;
using Urd.Services;

namespace Urd.Test
{
    public class TestClockService
    {
        private IClockService _clockService;

        private float _timeStamp;
        private bool _delayCallCalled;

        [SetUp]
        public void SetUp()
        {
            IServiceLocator serviceLocator = new ServiceLocator();
            var coroutineSubstituted = Substitute.For<ICoroutineService>();
            serviceLocator.Register<ICoroutineService>(coroutineSubstituted);
            _clockService = new ClockService();
            _clockService.SetServiceLocatorService(serviceLocator);
            _clockService.Init();
            _timeStamp = 0;
            _delayCallCalled = false;
        }

        [Test]
        public void ClockService_SuscribeToUpdate_Success()
        {
            _clockService.SuscribeToUpdate(DummyUpdate);
            float deltaTime = 0.1f;

            _clockService.Update(deltaTime);

            Assert.That(_timeStamp, Is.EqualTo(deltaTime));
        }

        [Test]
        public void ClockService_UnsuscribeToUpdate_Success()
        {
            _clockService.SuscribeToUpdate(DummyUpdate);
            _clockService.UnSuscribeToUpdate(DummyUpdate);
            float deltaTime = 0.1f;

            _clockService.Update(deltaTime);

            Assert.That(_timeStamp, Is.Not.EqualTo(deltaTime));
        }

        [Test]
        public void ClockService_SuscribeToUpdateButPause_Success()
        {
            _clockService.SuscribeToUpdate(DummyUpdate, true);
            float deltaTime = 0.1f;

            _clockService.SetPause(true);
            _clockService.Update(deltaTime);

            Assert.That(_timeStamp, Is.EqualTo(0));
        }

        [Test]
        public void ClockService_SuscribeToUpdateButUnPause_Success()
        {
            _clockService.SuscribeToUpdate(DummyUpdate, true);
            float deltaTime = 0.1f;

            _clockService.SetPause(true);
            _clockService.Update(deltaTime);
            _clockService.SetPause(false);
            _clockService.Update(deltaTime);

            Assert.That(_timeStamp, Is.EqualTo(deltaTime));
        }

        [Test]
        [TestCase(0.2f, 1f, true)]
        [TestCase(0.2f, 0.1f, false)]
        public void ClockService_AddDelayCall_Finish(float delayCall, float deltaTime, bool called)
        {
            _clockService.AddDelayCall(delayCall, DelayCallDummy);

            _clockService.Update(deltaTime);

            Assert.That(_delayCallCalled, Is.EqualTo(called));
        }

        private void DummyUpdate(float deltaTime)
        {
            _timeStamp += deltaTime;
        }

        private void DelayCallDummy()
        {
            _delayCallCalled = true;
        }

    }
}