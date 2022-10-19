using NUnit.Framework;
using Urd.Services;
using Urd.Services.EventBus;

namespace Urd.Test
{
    public class TestEventBusService
    {
        IEventBusService _eventBusService;
        
        DummyMessage _dummyMessage;
        DummyMessageTwo _dummyMessageTwo;

        public static int DummyMessageTimesCalled;
        public static int DummyMessageTwoTimesCalled;

        [SetUp]
        public void SetUp()
        {
            _eventBusService = new EventBusService();
           
            _dummyMessage = new DummyMessage();
            _dummyMessageTwo = new DummyMessageTwo();

            DummyMessageTimesCalled = 0;
            DummyMessageTwoTimesCalled = 0;
        }

        [Test]
        public void EventBusService_SubscribeSingle_Success()
        {
            var dummySingleObserver = new DummyObserverOneType();
            _eventBusService.Subscribe(dummySingleObserver);

            _eventBusService.Call(_dummyMessage);

            Assert.That(DummyMessageTimesCalled, Is.EqualTo(1));
        }

        [Test]
        public void EventBusService_SubscribeMultiple_Success()
        {
            var dummyMultipleObserver = new DummyObserverMultipleType();
            _eventBusService.Subscribe(dummyMultipleObserver);

            _eventBusService.Call(_dummyMessage);
            _eventBusService.Call(_dummyMessageTwo);

            Assert.That(DummyMessageTimesCalled + DummyMessageTwoTimesCalled, Is.EqualTo(2));
        }

        [Test]
        public void EventBusService_SubscribeSingleOneType_Success()
        {
            var dummySingleObserver = new DummyObserverOneType();
            _eventBusService.Subscribe(dummySingleObserver, typeof(DummyMessage));

            _eventBusService.Call(_dummyMessage);

            Assert.That(DummyMessageTimesCalled, Is.EqualTo(1));
        }

        [Test]
        public void EventBusService_SubscribeMultipleOneType_Success()
        {
            var dummyMultipleObserver = new DummyObserverMultipleType();
            _eventBusService.Subscribe(dummyMultipleObserver, typeof(DummyMessage));

            _eventBusService.Call(_dummyMessage);
            _eventBusService.Call(_dummyMessageTwo);

            Assert.That(DummyMessageTimesCalled + DummyMessageTwoTimesCalled, Is.EqualTo(1));
        }

        [Test]
        public void EventBusService_UnsubscribeSingle_Success()
        {
            var dummySingleObserver = new DummyObserverOneType();
            _eventBusService.Subscribe(dummySingleObserver);
            _eventBusService.Call(_dummyMessage);

            _eventBusService.Unsubscribe(dummySingleObserver);
            _eventBusService.Call(_dummyMessage);

            Assert.That(DummyMessageTimesCalled, Is.EqualTo(1));
        }

        [Test]
        public void EventBusService_UnsubscribeMultiple_Success()
        {
            var dummyMultipleObserver = new DummyObserverMultipleType();
            _eventBusService.Subscribe(dummyMultipleObserver);
            _eventBusService.Call(_dummyMessage);
            _eventBusService.Call(_dummyMessageTwo);

            _eventBusService.Unsubscribe(dummyMultipleObserver);
            _eventBusService.Call(_dummyMessage);
            _eventBusService.Call(_dummyMessageTwo);

            Assert.That(DummyMessageTimesCalled + DummyMessageTwoTimesCalled, Is.EqualTo(2));
        }

        [Test]
        public void EventBusService_UnsubscribeSingleOneType_Success()
        {
            var dummySingleObserver = new DummyObserverOneType();
            _eventBusService.Subscribe(dummySingleObserver, typeof(DummyMessage));
            _eventBusService.Call(_dummyMessage);

            _eventBusService.Unsubscribe(dummySingleObserver, typeof(DummyMessage));
            _eventBusService.Call(_dummyMessage);

            Assert.That(DummyMessageTimesCalled, Is.EqualTo(1));
        }

        [Test]
        public void EventBusService_UnsubscribeMultipleOneTypeSuscribe_Success()
        {
            var dummyMultipleObserver = new DummyObserverMultipleType();
            _eventBusService.Subscribe(dummyMultipleObserver, typeof(DummyMessage));
            _eventBusService.Call(_dummyMessage);
            _eventBusService.Call(_dummyMessageTwo);

            _eventBusService.Unsubscribe(dummyMultipleObserver, typeof(DummyMessage));
            _eventBusService.Call(_dummyMessage);
            _eventBusService.Call(_dummyMessageTwo);

            Assert.That(DummyMessageTimesCalled + DummyMessageTwoTimesCalled, Is.EqualTo(1));
        }

        [Test]
        public void EventBusService_UnsubscribeMultipleOneType_Success()
        {
            var dummyMultipleObserver = new DummyObserverMultipleType();
            _eventBusService.Subscribe(dummyMultipleObserver);
            _eventBusService.Call(_dummyMessage);
            _eventBusService.Call(_dummyMessageTwo);

            _eventBusService.Unsubscribe(dummyMultipleObserver, typeof(DummyMessage));
            _eventBusService.Call(_dummyMessage);
            _eventBusService.Call(_dummyMessageTwo);

            Assert.That(DummyMessageTimesCalled + DummyMessageTwoTimesCalled, Is.EqualTo(3));
        }

        private class DummyMessage : IEventBusMessage 
        { 
            public bool BoolVariable { get; set; }
        }
        private class DummyMessageTwo : IEventBusMessage
        {
            public bool BoolVariable { get; set; }
        }

        private class DummyObserverOneType : IEventBusObservable<DummyMessage>
        {
            public void OnNewEvent(DummyMessage newEvent)
            {
                DummyMessageTimesCalled++;
            }
        }
        private class DummyObserverMultipleType : IEventBusObservable<DummyMessage>, IEventBusObservable<DummyMessageTwo>
        {
            public void OnNewEvent(DummyMessage newEvent)
            {
                DummyMessageTimesCalled++;
            }

            public void OnNewEvent(DummyMessageTwo newEvent)
            {
                DummyMessageTwoTimesCalled++;
            }
        }

    }
}