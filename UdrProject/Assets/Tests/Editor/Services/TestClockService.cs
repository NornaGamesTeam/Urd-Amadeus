using NUnit.Framework;
using System;
using System.Collections;
using UnityEngine;
using Urd.Services;

public class TestClockService
{
    private IClockService _clockService;

    private float _timeStamp;

    [SetUp]
    public void SetUp()
    {
        IServiceLocator serviceLocator = new ServiceLocator();
        serviceLocator.Register<ICoroutineService>(new EditorCoroutineService());
        _clockService = new ClockService();
        _clockService.SetServiceLocatorService(serviceLocator);
        _clockService.Init();
        _timeStamp = 0;
    }

    [Test]
    public void ClockService_SuscribeToUpdate_Success()
    {
        _clockService.SuscribeToUpdate(DummyUpdate);

        Assert.That(_timeStamp, Is.Not.EqualTo(0));
    }

    private void DummyUpdate(float deltaTime)
    {
        _timeStamp += deltaTime;
    }
}
