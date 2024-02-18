//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Input/InputMap.inputactions
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

public partial class @InputMap: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMap()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMap"",
    ""maps"": [
        {
            ""name"": ""KeyboardMap"",
            ""id"": ""7787575f-0876-4928-ad40-1c3a883e12ca"",
            ""actions"": [
                {
                    ""name"": ""MoveAction"",
                    ""type"": ""Value"",
                    ""id"": ""0e03dd8e-6104-4b16-a098-7009d62fade8"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""DamageAction"",
                    ""type"": ""Button"",
                    ""id"": ""bc83afe8-bc83-41ec-8044-91bc40118f56"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ShootAction"",
                    ""type"": ""Button"",
                    ""id"": ""9b95c75d-843d-4b43-95f2-ea5666d89b9c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SpawnMedalAction"",
                    ""type"": ""Button"",
                    ""id"": ""7e876d48-bbbe-4148-87be-728aeebcf2a8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""OpenChestAction"",
                    ""type"": ""Value"",
                    ""id"": ""efafa619-bb64-4c3c-a4c3-77ea25073b65"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD Movement"",
                    ""id"": ""3d91bccd-5994-4068-a513-c95f267a0966"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveAction"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""69060a60-33ed-4df9-8afc-b1bbe36c1c8c"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardInputs"",
                    ""action"": ""MoveAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""1b2eaa9b-19bd-4ffc-a7aa-6d3863722537"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardInputs"",
                    ""action"": ""MoveAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""8a78546b-941b-4231-936b-c8ffb83530a6"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardInputs"",
                    ""action"": ""MoveAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""6dc1ed19-2953-4474-8b94-e5fb9822caa6"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardInputs"",
                    ""action"": ""MoveAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""fb31fdcc-bcae-4f96-a728-4c232f46956e"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardInputs"",
                    ""action"": ""DamageAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6d2775de-0e66-463b-81a9-b9fa120cb532"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardInputs"",
                    ""action"": ""ShootAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a8421cd7-4ee8-437f-b77c-d13ec51c1575"",
                    ""path"": ""<Keyboard>/m"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardInputs"",
                    ""action"": ""SpawnMedalAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fe729cf3-1499-40c5-ae5a-569fae627096"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardInputs"",
                    ""action"": ""OpenChestAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KeyboardInputs"",
            ""bindingGroup"": ""KeyboardInputs"",
            ""devices"": []
        }
    ]
}");
        // KeyboardMap
        m_KeyboardMap = asset.FindActionMap("KeyboardMap", throwIfNotFound: true);
        m_KeyboardMap_MoveAction = m_KeyboardMap.FindAction("MoveAction", throwIfNotFound: true);
        m_KeyboardMap_DamageAction = m_KeyboardMap.FindAction("DamageAction", throwIfNotFound: true);
        m_KeyboardMap_ShootAction = m_KeyboardMap.FindAction("ShootAction", throwIfNotFound: true);
        m_KeyboardMap_SpawnMedalAction = m_KeyboardMap.FindAction("SpawnMedalAction", throwIfNotFound: true);
        m_KeyboardMap_OpenChestAction = m_KeyboardMap.FindAction("OpenChestAction", throwIfNotFound: true);
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

    // KeyboardMap
    private readonly InputActionMap m_KeyboardMap;
    private List<IKeyboardMapActions> m_KeyboardMapActionsCallbackInterfaces = new List<IKeyboardMapActions>();
    private readonly InputAction m_KeyboardMap_MoveAction;
    private readonly InputAction m_KeyboardMap_DamageAction;
    private readonly InputAction m_KeyboardMap_ShootAction;
    private readonly InputAction m_KeyboardMap_SpawnMedalAction;
    private readonly InputAction m_KeyboardMap_OpenChestAction;
    public struct KeyboardMapActions
    {
        private @InputMap m_Wrapper;
        public KeyboardMapActions(@InputMap wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveAction => m_Wrapper.m_KeyboardMap_MoveAction;
        public InputAction @DamageAction => m_Wrapper.m_KeyboardMap_DamageAction;
        public InputAction @ShootAction => m_Wrapper.m_KeyboardMap_ShootAction;
        public InputAction @SpawnMedalAction => m_Wrapper.m_KeyboardMap_SpawnMedalAction;
        public InputAction @OpenChestAction => m_Wrapper.m_KeyboardMap_OpenChestAction;
        public InputActionMap Get() { return m_Wrapper.m_KeyboardMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(KeyboardMapActions set) { return set.Get(); }
        public void AddCallbacks(IKeyboardMapActions instance)
        {
            if (instance == null || m_Wrapper.m_KeyboardMapActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_KeyboardMapActionsCallbackInterfaces.Add(instance);
            @MoveAction.started += instance.OnMoveAction;
            @MoveAction.performed += instance.OnMoveAction;
            @MoveAction.canceled += instance.OnMoveAction;
            @DamageAction.started += instance.OnDamageAction;
            @DamageAction.performed += instance.OnDamageAction;
            @DamageAction.canceled += instance.OnDamageAction;
            @ShootAction.started += instance.OnShootAction;
            @ShootAction.performed += instance.OnShootAction;
            @ShootAction.canceled += instance.OnShootAction;
            @SpawnMedalAction.started += instance.OnSpawnMedalAction;
            @SpawnMedalAction.performed += instance.OnSpawnMedalAction;
            @SpawnMedalAction.canceled += instance.OnSpawnMedalAction;
            @OpenChestAction.started += instance.OnOpenChestAction;
            @OpenChestAction.performed += instance.OnOpenChestAction;
            @OpenChestAction.canceled += instance.OnOpenChestAction;
        }

        private void UnregisterCallbacks(IKeyboardMapActions instance)
        {
            @MoveAction.started -= instance.OnMoveAction;
            @MoveAction.performed -= instance.OnMoveAction;
            @MoveAction.canceled -= instance.OnMoveAction;
            @DamageAction.started -= instance.OnDamageAction;
            @DamageAction.performed -= instance.OnDamageAction;
            @DamageAction.canceled -= instance.OnDamageAction;
            @ShootAction.started -= instance.OnShootAction;
            @ShootAction.performed -= instance.OnShootAction;
            @ShootAction.canceled -= instance.OnShootAction;
            @SpawnMedalAction.started -= instance.OnSpawnMedalAction;
            @SpawnMedalAction.performed -= instance.OnSpawnMedalAction;
            @SpawnMedalAction.canceled -= instance.OnSpawnMedalAction;
            @OpenChestAction.started -= instance.OnOpenChestAction;
            @OpenChestAction.performed -= instance.OnOpenChestAction;
            @OpenChestAction.canceled -= instance.OnOpenChestAction;
        }

        public void RemoveCallbacks(IKeyboardMapActions instance)
        {
            if (m_Wrapper.m_KeyboardMapActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IKeyboardMapActions instance)
        {
            foreach (var item in m_Wrapper.m_KeyboardMapActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_KeyboardMapActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public KeyboardMapActions @KeyboardMap => new KeyboardMapActions(this);
    private int m_KeyboardInputsSchemeIndex = -1;
    public InputControlScheme KeyboardInputsScheme
    {
        get
        {
            if (m_KeyboardInputsSchemeIndex == -1) m_KeyboardInputsSchemeIndex = asset.FindControlSchemeIndex("KeyboardInputs");
            return asset.controlSchemes[m_KeyboardInputsSchemeIndex];
        }
    }
    public interface IKeyboardMapActions
    {
        void OnMoveAction(InputAction.CallbackContext context);
        void OnDamageAction(InputAction.CallbackContext context);
        void OnShootAction(InputAction.CallbackContext context);
        void OnSpawnMedalAction(InputAction.CallbackContext context);
        void OnOpenChestAction(InputAction.CallbackContext context);
    }
}
