// GENERATED AUTOMATICALLY FROM 'Assets/Inputs/StandardControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @StandardControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @StandardControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""StandardControls"",
    ""maps"": [
        {
            ""name"": ""Movement"",
            ""id"": ""9680937b-24f1-44b6-b69e-4a4b303d0517"",
            ""actions"": [
                {
                    ""name"": ""Axis"",
                    ""type"": ""Value"",
                    ""id"": ""cca57883-5a35-4ac1-acb0-eb97d678a342"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""de952262-08f0-4ee4-a013-a6b71269d808"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""f6854e32-2918-4047-8352-ed182c84cd92"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""ScaleVector2(x=5,y=0.05),InvertVector2"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Recenter"",
                    ""type"": ""Button"",
                    ""id"": ""1098275a-b594-4f05-89e3-737b308da976"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""f8025294-998c-4c72-9690-5d727f466737"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Axis"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""fcb8a096-066c-4e5a-af2f-16da92d2df5e"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Axis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""9b6edb3f-62dc-4fe0-a494-718b44652e63"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Axis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""4ddbc98d-4747-4f24-8604-2fa2cfd24010"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Axis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""39a723fe-9607-4c3f-a8a7-c37f044e3aed"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Axis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""3d6f9f53-2cc2-4402-93ef-09207af5d0ec"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""803ed08b-cd6e-4ddb-8529-edb0768ecb8e"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": ""ScaleVector2(x=0.1,y=0.1)"",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""588204e1-dd06-4722-ab11-6274853623d9"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Recenter"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Movement
        m_Movement = asset.FindActionMap("Movement", throwIfNotFound: true);
        m_Movement_Axis = m_Movement.FindAction("Axis", throwIfNotFound: true);
        m_Movement_Jump = m_Movement.FindAction("Jump", throwIfNotFound: true);
        m_Movement_Look = m_Movement.FindAction("Look", throwIfNotFound: true);
        m_Movement_Recenter = m_Movement.FindAction("Recenter", throwIfNotFound: true);
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

    // Movement
    private readonly InputActionMap m_Movement;
    private IMovementActions m_MovementActionsCallbackInterface;
    private readonly InputAction m_Movement_Axis;
    private readonly InputAction m_Movement_Jump;
    private readonly InputAction m_Movement_Look;
    private readonly InputAction m_Movement_Recenter;
    public struct MovementActions
    {
        private @StandardControls m_Wrapper;
        public MovementActions(@StandardControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Axis => m_Wrapper.m_Movement_Axis;
        public InputAction @Jump => m_Wrapper.m_Movement_Jump;
        public InputAction @Look => m_Wrapper.m_Movement_Look;
        public InputAction @Recenter => m_Wrapper.m_Movement_Recenter;
        public InputActionMap Get() { return m_Wrapper.m_Movement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MovementActions set) { return set.Get(); }
        public void SetCallbacks(IMovementActions instance)
        {
            if (m_Wrapper.m_MovementActionsCallbackInterface != null)
            {
                @Axis.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnAxis;
                @Axis.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnAxis;
                @Axis.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnAxis;
                @Jump.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnJump;
                @Look.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnLook;
                @Recenter.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnRecenter;
                @Recenter.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnRecenter;
                @Recenter.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnRecenter;
            }
            m_Wrapper.m_MovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Axis.started += instance.OnAxis;
                @Axis.performed += instance.OnAxis;
                @Axis.canceled += instance.OnAxis;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @Recenter.started += instance.OnRecenter;
                @Recenter.performed += instance.OnRecenter;
                @Recenter.canceled += instance.OnRecenter;
            }
        }
    }
    public MovementActions @Movement => new MovementActions(this);
    public interface IMovementActions
    {
        void OnAxis(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnRecenter(InputAction.CallbackContext context);
    }
}
