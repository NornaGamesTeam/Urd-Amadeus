using NUnit.Framework;
using System.Collections;
using UnityEngine;
using Urd.Services;

public class TestCoroutineService
{
    ICoroutineService _coroutineService;

    private bool _coroutineCalled;
    private Coroutine _twoSecondsCoroutine;
    private bool _coroutineTwoSeconds;

    [SetUp]
    public void SetUp()
    {
        _coroutineService = new CoroutineService();
        _coroutineService.Init();

        _coroutineCalled = false;
        _coroutineTwoSeconds = false;
    }

    [Test]
    public void CoroutineService_StartCoroutine_Success()
    {
        _coroutineService.StartCoroutine(DummyCoroutineCo());

        Assert.That(_coroutineCalled, Is.True);
    }

    [Test]
    public void CoroutineService_StartCoroutineWaitTwoSeconds_Success()
    {
        _coroutineService.StartCoroutine(TwoSecondsCoroutineCo());
        _coroutineTwoSeconds = true;
        _coroutineService.StartCoroutine(CoroutineService_StartCoroutineWaitTwoSeconds_SuccessCo());
    }

    [Test]
    public void CoroutineService_StopCoroutine_Success()
    {
        _twoSecondsCoroutine = _coroutineService.StartCoroutine(TwoSecondsCoroutineCo());

        _coroutineService.StopCoroutine(_twoSecondsCoroutine);

        Assert.That(_coroutineTwoSeconds, Is.True);
    }
    
    [Test]
    public void CoroutineService_StopAllCoroutine_Success()
    {
        _twoSecondsCoroutine = _coroutineService.StartCoroutine(TwoSecondsCoroutineCo());

        _coroutineService.StopAllCoroutines();

        Assert.That(_coroutineTwoSeconds, Is.True);
    }

    private IEnumerator DummyCoroutineCo()
    {
        _coroutineCalled = true;
        yield return 0;
        _coroutineCalled = false;
    }

    private IEnumerator TwoSecondsCoroutineCo()
    {
        _coroutineTwoSeconds = true;
        yield return new WaitForSeconds(2);
        _coroutineTwoSeconds = false;
    }

    public IEnumerator CoroutineService_StartCoroutineWaitTwoSeconds_SuccessCo()
    {
        yield return new WaitForSeconds(2);
        Assert.That(_coroutineTwoSeconds, Is.False);
    }
}
