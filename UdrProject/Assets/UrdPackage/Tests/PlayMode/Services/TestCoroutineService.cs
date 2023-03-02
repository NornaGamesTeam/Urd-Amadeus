using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using Urd.Services;

namespace Urd.Test
{
    public class TestCoroutineService
    {
        ICoroutineService _coroutineService;

        private bool _coroutineCalled;
        private bool _coroutineWaitSeconds;
        private Coroutine _waitCoroutine;

        [SetUp]
        public void SetUp()
        {
            _coroutineService = new CoroutineService();
            _coroutineService.Init();

            _coroutineCalled = false;
            _coroutineWaitSeconds = false;
        }

        [UnityTest]
        public IEnumerator CoroutineService_StartCoroutine_Success()
        {
            _coroutineService.StartCoroutine(DummyCoroutineCo());
            
            yield return null;
            
            Assert.That(_coroutineCalled, Is.True);
        }

        [UnityTest]
        public IEnumerator CoroutineService_StartCoroutineWait_Success()
        {
            float delayTime = 0.1f;
            float timeWaiting = 0.2f;
            _coroutineService.StartCoroutine(WaitSecondsCoroutineCo(delayTime));

            yield return new WaitForSeconds(timeWaiting);

            Assert.That(_coroutineWaitSeconds, Is.True);
        }

        [UnityTest]
        public IEnumerator CoroutineService_StartCoroutineWait_Failed()
        {
            float delayTime = 0.2f;
            float timeWaiting = 0.1f;
            _coroutineService.StartCoroutine(WaitSecondsCoroutineCo(delayTime));

            yield return new WaitForSeconds(timeWaiting);

            Assert.That(_coroutineWaitSeconds, Is.False);
        }

        [UnityTest]
        public IEnumerator CoroutineService_StopCoroutine_Success()
        {
            _waitCoroutine = _coroutineService.StartCoroutine(DummyCoroutineCo());
            _coroutineCalled = false;

            _coroutineService.StopCoroutine(_waitCoroutine);
            yield return null;

            Assert.That(_coroutineWaitSeconds, Is.False);
        }

        [UnityTest]
        public IEnumerator CoroutineService_StopAllCoroutine_Success()
        {
            _waitCoroutine = _coroutineService.StartCoroutine(DummyCoroutineCo());
            _coroutineCalled = false;

            _coroutineService.StopAllCoroutines();
            yield return null;

            Assert.That(_coroutineWaitSeconds, Is.False);
        }

        private IEnumerator DummyCoroutineCo()
        {
            yield return 0;
            _coroutineCalled = true;
        }

        private IEnumerator WaitSecondsCoroutineCo(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            _coroutineWaitSeconds = true;
        }
    }
}