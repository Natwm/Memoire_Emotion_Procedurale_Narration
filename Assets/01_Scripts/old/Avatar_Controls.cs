// GENERATED AUTOMATICALLY FROM 'Assets/01_Scripts/old/Avatar_Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Avatar_Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Avatar_Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Avatar_Controls"",
    ""maps"": [
        {
            ""name"": ""Movement"",
            ""id"": ""367cc20b-940b-4604-81a9-b1ad9c099371"",
            ""actions"": [
                {
                    ""name"": ""MoveVector"",
                    ""type"": ""Value"",
                    ""id"": ""410507a2-86ca-43a5-9f29-62aafb7c2e81"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Intensity"",
                    ""type"": ""Value"",
                    ""id"": ""1039250a-e434-40b5-9d78-85111b96e2a8"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Left_Bumper"",
                    ""type"": ""Button"",
                    ""id"": ""271ef86d-a476-48c2-b140-bd4e8bed613a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Right_Bumper"",
                    ""type"": ""Button"",
                    ""id"": ""534a43bb-7e2b-4daa-ba93-16144543ac8f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Button_Y"",
                    ""type"": ""Button"",
                    ""id"": ""ee9b797b-630c-4181-b71c-da0a027ec492"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Button_B"",
                    ""type"": ""Button"",
                    ""id"": ""b5ed2d14-a38d-4fe4-aabd-6f49d1ac5f51"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Button_A"",
                    ""type"": ""Button"",
                    ""id"": ""260221a6-fbec-4f08-a322-25ad7ff1d268"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Button_X"",
                    ""type"": ""Button"",
                    ""id"": ""3d98f90f-ed7e-4d5c-b74b-bac63b5a196a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightJoystick"",
                    ""type"": ""Value"",
                    ""id"": ""084d364e-2be5-49f0-b836-d4f6805b53c4"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f2891ecb-25dd-4d8c-b1c1-2699a7220546"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""MoveVector"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c2b3cafc-5a93-449b-859a-9a5918d6e524"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Intensity"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""abc0c17e-30db-44d9-a522-c0d1ed70e967"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Left_Bumper"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""47266221-c0f7-4393-aa5e-26b70425d445"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Right_Bumper"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9d1061d0-1b5f-4b21-a903-f59de73d2328"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Button_Y"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e418071d-f4ed-4f14-8759-6bb300cedc67"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Button_B"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""21eaef56-1a34-4f55-9ae8-8def15bad697"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Button_A"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""14bddc6b-7d70-4d43-acde-783ab54bfb24"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Button_X"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e5b0e4fa-d3a8-4ca0-b643-def4e4b40fdc"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""RightJoystick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Controller"",
            ""bindingGroup"": ""Controller"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Movement
        m_Movement = asset.FindActionMap("Movement", throwIfNotFound: true);
        m_Movement_MoveVector = m_Movement.FindAction("MoveVector", throwIfNotFound: true);
        m_Movement_Intensity = m_Movement.FindAction("Intensity", throwIfNotFound: true);
        m_Movement_Left_Bumper = m_Movement.FindAction("Left_Bumper", throwIfNotFound: true);
        m_Movement_Right_Bumper = m_Movement.FindAction("Right_Bumper", throwIfNotFound: true);
        m_Movement_Button_Y = m_Movement.FindAction("Button_Y", throwIfNotFound: true);
        m_Movement_Button_B = m_Movement.FindAction("Button_B", throwIfNotFound: true);
        m_Movement_Button_A = m_Movement.FindAction("Button_A", throwIfNotFound: true);
        m_Movement_Button_X = m_Movement.FindAction("Button_X", throwIfNotFound: true);
        m_Movement_RightJoystick = m_Movement.FindAction("RightJoystick", throwIfNotFound: true);
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
    private readonly InputAction m_Movement_MoveVector;
    private readonly InputAction m_Movement_Intensity;
    private readonly InputAction m_Movement_Left_Bumper;
    private readonly InputAction m_Movement_Right_Bumper;
    private readonly InputAction m_Movement_Button_Y;
    private readonly InputAction m_Movement_Button_B;
    private readonly InputAction m_Movement_Button_A;
    private readonly InputAction m_Movement_Button_X;
    private readonly InputAction m_Movement_RightJoystick;
    public struct MovementActions
    {
        private @Avatar_Controls m_Wrapper;
        public MovementActions(@Avatar_Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveVector => m_Wrapper.m_Movement_MoveVector;
        public InputAction @Intensity => m_Wrapper.m_Movement_Intensity;
        public InputAction @Left_Bumper => m_Wrapper.m_Movement_Left_Bumper;
        public InputAction @Right_Bumper => m_Wrapper.m_Movement_Right_Bumper;
        public InputAction @Button_Y => m_Wrapper.m_Movement_Button_Y;
        public InputAction @Button_B => m_Wrapper.m_Movement_Button_B;
        public InputAction @Button_A => m_Wrapper.m_Movement_Button_A;
        public InputAction @Button_X => m_Wrapper.m_Movement_Button_X;
        public InputAction @RightJoystick => m_Wrapper.m_Movement_RightJoystick;
        public InputActionMap Get() { return m_Wrapper.m_Movement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MovementActions set) { return set.Get(); }
        public void SetCallbacks(IMovementActions instance)
        {
            if (m_Wrapper.m_MovementActionsCallbackInterface != null)
            {
                @MoveVector.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnMoveVector;
                @MoveVector.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnMoveVector;
                @MoveVector.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnMoveVector;
                @Intensity.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnIntensity;
                @Intensity.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnIntensity;
                @Intensity.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnIntensity;
                @Left_Bumper.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnLeft_Bumper;
                @Left_Bumper.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnLeft_Bumper;
                @Left_Bumper.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnLeft_Bumper;
                @Right_Bumper.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnRight_Bumper;
                @Right_Bumper.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnRight_Bumper;
                @Right_Bumper.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnRight_Bumper;
                @Button_Y.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnButton_Y;
                @Button_Y.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnButton_Y;
                @Button_Y.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnButton_Y;
                @Button_B.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnButton_B;
                @Button_B.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnButton_B;
                @Button_B.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnButton_B;
                @Button_A.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnButton_A;
                @Button_A.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnButton_A;
                @Button_A.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnButton_A;
                @Button_X.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnButton_X;
                @Button_X.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnButton_X;
                @Button_X.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnButton_X;
                @RightJoystick.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnRightJoystick;
                @RightJoystick.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnRightJoystick;
                @RightJoystick.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnRightJoystick;
            }
            m_Wrapper.m_MovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveVector.started += instance.OnMoveVector;
                @MoveVector.performed += instance.OnMoveVector;
                @MoveVector.canceled += instance.OnMoveVector;
                @Intensity.started += instance.OnIntensity;
                @Intensity.performed += instance.OnIntensity;
                @Intensity.canceled += instance.OnIntensity;
                @Left_Bumper.started += instance.OnLeft_Bumper;
                @Left_Bumper.performed += instance.OnLeft_Bumper;
                @Left_Bumper.canceled += instance.OnLeft_Bumper;
                @Right_Bumper.started += instance.OnRight_Bumper;
                @Right_Bumper.performed += instance.OnRight_Bumper;
                @Right_Bumper.canceled += instance.OnRight_Bumper;
                @Button_Y.started += instance.OnButton_Y;
                @Button_Y.performed += instance.OnButton_Y;
                @Button_Y.canceled += instance.OnButton_Y;
                @Button_B.started += instance.OnButton_B;
                @Button_B.performed += instance.OnButton_B;
                @Button_B.canceled += instance.OnButton_B;
                @Button_A.started += instance.OnButton_A;
                @Button_A.performed += instance.OnButton_A;
                @Button_A.canceled += instance.OnButton_A;
                @Button_X.started += instance.OnButton_X;
                @Button_X.performed += instance.OnButton_X;
                @Button_X.canceled += instance.OnButton_X;
                @RightJoystick.started += instance.OnRightJoystick;
                @RightJoystick.performed += instance.OnRightJoystick;
                @RightJoystick.canceled += instance.OnRightJoystick;
            }
        }
    }
    public MovementActions @Movement => new MovementActions(this);
    private int m_ControllerSchemeIndex = -1;
    public InputControlScheme ControllerScheme
    {
        get
        {
            if (m_ControllerSchemeIndex == -1) m_ControllerSchemeIndex = asset.FindControlSchemeIndex("Controller");
            return asset.controlSchemes[m_ControllerSchemeIndex];
        }
    }
    public interface IMovementActions
    {
        void OnMoveVector(InputAction.CallbackContext context);
        void OnIntensity(InputAction.CallbackContext context);
        void OnLeft_Bumper(InputAction.CallbackContext context);
        void OnRight_Bumper(InputAction.CallbackContext context);
        void OnButton_Y(InputAction.CallbackContext context);
        void OnButton_B(InputAction.CallbackContext context);
        void OnButton_A(InputAction.CallbackContext context);
        void OnButton_X(InputAction.CallbackContext context);
        void OnRightJoystick(InputAction.CallbackContext context);
    }
}
