using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Rendering;
using UnityEngine.TestTools;
using UnityEngine.UIElements;
using Urd.Services;

namespace Urd.Test
{
    public class TestInputService
    {
        private IInputService _inputService;

        private string _actionName = "TestActionA";
        private bool _onPerfomedCallback;

        [SetUp]
        public void SetUp()
        {
            _inputService = new InputService();
            _inputService.Init();
        }

        [UnityTest]
        public IEnumerator InputService_SubscribeToAction_Success()
        {
            _inputService.SubscribeToActionOnPerformed(_actionName, OnPerformMethod);

            SimulateSpacePress();
            yield return new WaitUntil(() => _onPerfomedCallback);

            Assert.That(_onPerfomedCallback, Is.True);
        }

        private void SimulateSpacePress()
        {
            var device = InputSystem.devices[0];
            Debug.Log(device);
            InputEventPtr eventPtr;
            using (StateEvent.From(device, out eventPtr))
            {
                var keyboard = (device as Keyboard);
                ((KeyControl)device["a"]).WriteValueIntoEvent(1f, eventPtr);
                InputSystem.QueueEvent(eventPtr);
                
            }
        }

        private void OnPerformMethod(InputAction.CallbackContext context)
        {
            _onPerfomedCallback = context.performed;
        }
    }
}