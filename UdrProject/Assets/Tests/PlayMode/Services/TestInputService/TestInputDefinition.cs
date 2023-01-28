//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Tests/PlayMode/Services/TestInputService/TestInputDefinition.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Urd.Test
{
    public partial class @TestInputDefinition : IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @TestInputDefinition()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""TestInputDefinition"",
    ""maps"": [
        {
            ""name"": ""Editor"",
            ""id"": ""8e7f7c04-8229-43a7-8b8d-dfcbd4f6cb7f"",
            ""actions"": [
                {
                    ""name"": ""TestActionSpace"",
                    ""type"": ""Button"",
                    ""id"": ""d9409475-4b15-4a0b-9eb5-7e4434dfb604"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a1ebdcb9-beef-4015-ac32-084ae131abdd"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TestActionSpace"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Editor
            m_Editor = asset.FindActionMap("Editor", throwIfNotFound: true);
            m_Editor_TestActionSpace = m_Editor.FindAction("TestActionSpace", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }
        public IEnumerable<InputBinding> bindings => asset.bindings;

        public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
        {
            return asset.FindAction(actionNameOrId, throwIfNotFound);
        }
        public int FindBinding(InputBinding bindingMask, out InputAction action)
        {
            return asset.FindBinding(bindingMask, out action);
        }

        // Editor
        private readonly InputActionMap m_Editor;
        private IEditorActions m_EditorActionsCallbackInterface;
        private readonly InputAction m_Editor_TestActionSpace;
        public struct EditorActions
        {
            private @TestInputDefinition m_Wrapper;
            public EditorActions(@TestInputDefinition wrapper) { m_Wrapper = wrapper; }
            public InputAction @TestActionSpace => m_Wrapper.m_Editor_TestActionSpace;
            public InputActionMap Get() { return m_Wrapper.m_Editor; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(EditorActions set) { return set.Get(); }
            public void SetCallbacks(IEditorActions instance)
            {
                if (m_Wrapper.m_EditorActionsCallbackInterface != null)
                {
                    @TestActionSpace.started -= m_Wrapper.m_EditorActionsCallbackInterface.OnTestActionSpace;
                    @TestActionSpace.performed -= m_Wrapper.m_EditorActionsCallbackInterface.OnTestActionSpace;
                    @TestActionSpace.canceled -= m_Wrapper.m_EditorActionsCallbackInterface.OnTestActionSpace;
                }
                m_Wrapper.m_EditorActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @TestActionSpace.started += instance.OnTestActionSpace;
                    @TestActionSpace.performed += instance.OnTestActionSpace;
                    @TestActionSpace.canceled += instance.OnTestActionSpace;
                }
            }
        }
        public EditorActions @Editor => new EditorActions(this);
        public interface IEditorActions
        {
            void OnTestActionSpace(InputAction.CallbackContext context);
        }
    }
}