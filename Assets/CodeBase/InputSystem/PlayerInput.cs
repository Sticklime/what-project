//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.13.0
//     from Assets/CodeBase/InputSystem/PlayerInput.inputactions
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

/// <summary>
/// Provides programmatic access to <see cref="InputActionAsset" />, <see cref="InputActionMap" />, <see cref="InputAction" /> and <see cref="InputControlScheme" /> instances defined in asset "Assets/CodeBase/InputSystem/PlayerInput.inputactions".
/// </summary>
/// <remarks>
/// This class is source generated and any manual edits will be discarded if the associated asset is reimported or modified.
/// </remarks>
/// <example>
/// <code>
/// using namespace UnityEngine;
/// using UnityEngine.InputSystem;
///
/// // Example of using an InputActionMap named "Player" from a UnityEngine.MonoBehaviour implementing callback interface.
/// public class Example : MonoBehaviour, MyActions.IPlayerActions
/// {
///     private MyActions_Actions m_Actions;                  // Source code representation of asset.
///     private MyActions_Actions.PlayerActions m_Player;     // Source code representation of action map.
///
///     void Awake()
///     {
///         m_Actions = new MyActions_Actions();              // Create asset object.
///         m_Player = m_Actions.Player;                      // Extract action map object.
///         m_Player.AddCallbacks(this);                      // Register callback interface IPlayerActions.
///     }
///
///     void OnDestroy()
///     {
///         m_Actions.Dispose();                              // Destroy asset object.
///     }
///
///     void OnEnable()
///     {
///         m_Player.Enable();                                // Enable all actions within map.
///     }
///
///     void OnDisable()
///     {
///         m_Player.Disable();                               // Disable all actions within map.
///     }
///
///     #region Interface implementation of MyActions.IPlayerActions
///
///     // Invoked when "Move" action is either started, performed or canceled.
///     public void OnMove(InputAction.CallbackContext context)
///     {
///         Debug.Log($"OnMove: {context.ReadValue&lt;Vector2&gt;()}");
///     }
///
///     // Invoked when "Attack" action is either started, performed or canceled.
///     public void OnAttack(InputAction.CallbackContext context)
///     {
///         Debug.Log($"OnAttack: {context.ReadValue&lt;float&gt;()}");
///     }
///
///     #endregion
/// }
/// </code>
/// </example>
public partial class @PlayerInput: IInputActionCollection2, IDisposable
{
    /// <summary>
    /// Provides access to the underlying asset instance.
    /// </summary>
    public InputActionAsset asset { get; }

    /// <summary>
    /// Constructs a new instance.
    /// </summary>
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Camera"",
            ""id"": ""4052359e-09fb-4e3d-8f44-c074e9e31649"",
            ""actions"": [
                {
                    ""name"": ""MoveOnMiddleButton"",
                    ""type"": ""Button"",
                    ""id"": ""d2af452e-30b5-4035-bb5e-f55af0392571"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press,Hold"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""c98159dd-7a7c-4071-83fd-05832b324f75"",
                    ""expectedControlType"": ""Vector3"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Selection"",
                    ""type"": ""Button"",
                    ""id"": ""f129be2f-37de-43b8-b0b9-01a9619ec375"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SetTargetPosition"",
                    ""type"": ""Value"",
                    ""id"": ""77041fce-6b4f-42b2-a7d0-3250bb63e874"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""0d098f50-ceb8-4ecb-8221-a2423bf23742"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""RotateBuilding"",
                    ""type"": ""Button"",
                    ""id"": ""12127c06-5064-4fde-b3af-705964de38a7"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MouseDelta"",
                    ""type"": ""Value"",
                    ""id"": ""2aaa6b5c-4611-4189-8bca-ca680967b590"",
                    ""expectedControlType"": ""Vector3"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""167fa2ef-b42b-43c3-9fef-2af27a3d5e24"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Selection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""98c4ce8d-584d-4cb3-95a7-f6ee5af299f6"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Direction"",
                    ""id"": ""c5ffd9b9-905f-484c-961b-4725a393364f"",
                    ""path"": ""3DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""98214bdb-2ef4-429c-9072-07d8b6c74a1a"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""88738c8d-3db8-4af0-93ac-38424671c329"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""76e41461-a457-4388-8126-b0b3201f350a"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""d049e983-3efe-41b3-b76e-b9bf7bf8d45c"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""forward"",
                    ""id"": ""a25fad6f-7011-425e-8112-82cf6e333fd5"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""backward"",
                    ""id"": ""f34576d0-c777-4367-b8dd-8f47744d430e"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""c013a73c-7519-49cd-9311-94176008f4b5"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SetTargetPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3e506553-88aa-4004-9f19-cb125fb65704"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateBuilding"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1d5fb444-2feb-4651-b36f-026f1e2a9424"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveOnMiddleButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1f6da083-46fc-444b-92b7-1145a8c71e98"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseDelta"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Camera
        m_Camera = asset.FindActionMap("Camera", throwIfNotFound: true);
        m_Camera_MoveOnMiddleButton = m_Camera.FindAction("MoveOnMiddleButton", throwIfNotFound: true);
        m_Camera_Move = m_Camera.FindAction("Move", throwIfNotFound: true);
        m_Camera_Selection = m_Camera.FindAction("Selection", throwIfNotFound: true);
        m_Camera_SetTargetPosition = m_Camera.FindAction("SetTargetPosition", throwIfNotFound: true);
        m_Camera_MousePosition = m_Camera.FindAction("MousePosition", throwIfNotFound: true);
        m_Camera_RotateBuilding = m_Camera.FindAction("RotateBuilding", throwIfNotFound: true);
        m_Camera_MouseDelta = m_Camera.FindAction("MouseDelta", throwIfNotFound: true);
    }

    ~@PlayerInput()
    {
        UnityEngine.Debug.Assert(!m_Camera.enabled, "This will cause a leak and performance issues, PlayerInput.Camera.Disable() has not been called.");
    }

    /// <summary>
    /// Destroys this asset and all associated <see cref="InputAction"/> instances.
    /// </summary>
    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    /// <inheritdoc cref="UnityEngine.InputSystem.InputActionAsset.bindingMask" />
    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    /// <inheritdoc cref="UnityEngine.InputSystem.InputActionAsset.devices" />
    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    /// <inheritdoc cref="UnityEngine.InputSystem.InputActionAsset.controlSchemes" />
    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    /// <inheritdoc cref="UnityEngine.InputSystem.InputActionAsset.Contains(InputAction)" />
    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    /// <inheritdoc cref="UnityEngine.InputSystem.InputActionAsset.GetEnumerator()" />
    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    /// <inheritdoc cref="IEnumerable.GetEnumerator()" />
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <inheritdoc cref="UnityEngine.InputSystem.InputActionAsset.Enable()" />
    public void Enable()
    {
        asset.Enable();
    }

    /// <inheritdoc cref="UnityEngine.InputSystem.InputActionAsset.Disable()" />
    public void Disable()
    {
        asset.Disable();
    }

    /// <inheritdoc cref="UnityEngine.InputSystem.InputActionAsset.bindings" />
    public IEnumerable<InputBinding> bindings => asset.bindings;

    /// <inheritdoc cref="UnityEngine.InputSystem.InputActionAsset.FindAction(string, bool)" />
    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    /// <inheritdoc cref="UnityEngine.InputSystem.InputActionAsset.FindBinding(InputBinding, out InputAction)" />
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Camera
    private readonly InputActionMap m_Camera;
    private List<ICameraActions> m_CameraActionsCallbackInterfaces = new List<ICameraActions>();
    private readonly InputAction m_Camera_MoveOnMiddleButton;
    private readonly InputAction m_Camera_Move;
    private readonly InputAction m_Camera_Selection;
    private readonly InputAction m_Camera_SetTargetPosition;
    private readonly InputAction m_Camera_MousePosition;
    private readonly InputAction m_Camera_RotateBuilding;
    private readonly InputAction m_Camera_MouseDelta;
    /// <summary>
    /// Provides access to input actions defined in input action map "Camera".
    /// </summary>
    public struct CameraActions
    {
        private @PlayerInput m_Wrapper;

        /// <summary>
        /// Construct a new instance of the input action map wrapper class.
        /// </summary>
        public CameraActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        /// <summary>
        /// Provides access to the underlying input action "Camera/MoveOnMiddleButton".
        /// </summary>
        public InputAction @MoveOnMiddleButton => m_Wrapper.m_Camera_MoveOnMiddleButton;
        /// <summary>
        /// Provides access to the underlying input action "Camera/Move".
        /// </summary>
        public InputAction @Move => m_Wrapper.m_Camera_Move;
        /// <summary>
        /// Provides access to the underlying input action "Camera/Selection".
        /// </summary>
        public InputAction @Selection => m_Wrapper.m_Camera_Selection;
        /// <summary>
        /// Provides access to the underlying input action "Camera/SetTargetPosition".
        /// </summary>
        public InputAction @SetTargetPosition => m_Wrapper.m_Camera_SetTargetPosition;
        /// <summary>
        /// Provides access to the underlying input action "Camera/MousePosition".
        /// </summary>
        public InputAction @MousePosition => m_Wrapper.m_Camera_MousePosition;
        /// <summary>
        /// Provides access to the underlying input action "Camera/RotateBuilding".
        /// </summary>
        public InputAction @RotateBuilding => m_Wrapper.m_Camera_RotateBuilding;
        /// <summary>
        /// Provides access to the underlying input action "Camera/MouseDelta".
        /// </summary>
        public InputAction @MouseDelta => m_Wrapper.m_Camera_MouseDelta;
        /// <summary>
        /// Provides access to the underlying input action map instance.
        /// </summary>
        public InputActionMap Get() { return m_Wrapper.m_Camera; }
        /// <inheritdoc cref="UnityEngine.InputSystem.InputActionMap.Enable()" />
        public void Enable() { Get().Enable(); }
        /// <inheritdoc cref="UnityEngine.InputSystem.InputActionMap.Disable()" />
        public void Disable() { Get().Disable(); }
        /// <inheritdoc cref="UnityEngine.InputSystem.InputActionMap.enabled" />
        public bool enabled => Get().enabled;
        /// <summary>
        /// Implicitly converts an <see ref="CameraActions" /> to an <see ref="InputActionMap" /> instance.
        /// </summary>
        public static implicit operator InputActionMap(CameraActions set) { return set.Get(); }
        /// <summary>
        /// Adds <see cref="InputAction.started"/>, <see cref="InputAction.performed"/> and <see cref="InputAction.canceled"/> callbacks provided via <param cref="instance" /> on all input actions contained in this map.
        /// </summary>
        /// <param name="instance">Callback instance.</param>
        /// <remarks>
        /// If <paramref name="instance" /> is <c>null</c> or <paramref name="instance"/> have already been added this method does nothing.
        /// </remarks>
        /// <seealso cref="CameraActions" />
        public void AddCallbacks(ICameraActions instance)
        {
            if (instance == null || m_Wrapper.m_CameraActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_CameraActionsCallbackInterfaces.Add(instance);
            @MoveOnMiddleButton.started += instance.OnMoveOnMiddleButton;
            @MoveOnMiddleButton.performed += instance.OnMoveOnMiddleButton;
            @MoveOnMiddleButton.canceled += instance.OnMoveOnMiddleButton;
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Selection.started += instance.OnSelection;
            @Selection.performed += instance.OnSelection;
            @Selection.canceled += instance.OnSelection;
            @SetTargetPosition.started += instance.OnSetTargetPosition;
            @SetTargetPosition.performed += instance.OnSetTargetPosition;
            @SetTargetPosition.canceled += instance.OnSetTargetPosition;
            @MousePosition.started += instance.OnMousePosition;
            @MousePosition.performed += instance.OnMousePosition;
            @MousePosition.canceled += instance.OnMousePosition;
            @RotateBuilding.started += instance.OnRotateBuilding;
            @RotateBuilding.performed += instance.OnRotateBuilding;
            @RotateBuilding.canceled += instance.OnRotateBuilding;
            @MouseDelta.started += instance.OnMouseDelta;
            @MouseDelta.performed += instance.OnMouseDelta;
            @MouseDelta.canceled += instance.OnMouseDelta;
        }

        /// <summary>
        /// Removes <see cref="InputAction.started"/>, <see cref="InputAction.performed"/> and <see cref="InputAction.canceled"/> callbacks provided via <param cref="instance" /> on all input actions contained in this map.
        /// </summary>
        /// <remarks>
        /// Calling this method when <paramref name="instance" /> have not previously been registered has no side-effects.
        /// </remarks>
        /// <seealso cref="CameraActions" />
        private void UnregisterCallbacks(ICameraActions instance)
        {
            @MoveOnMiddleButton.started -= instance.OnMoveOnMiddleButton;
            @MoveOnMiddleButton.performed -= instance.OnMoveOnMiddleButton;
            @MoveOnMiddleButton.canceled -= instance.OnMoveOnMiddleButton;
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Selection.started -= instance.OnSelection;
            @Selection.performed -= instance.OnSelection;
            @Selection.canceled -= instance.OnSelection;
            @SetTargetPosition.started -= instance.OnSetTargetPosition;
            @SetTargetPosition.performed -= instance.OnSetTargetPosition;
            @SetTargetPosition.canceled -= instance.OnSetTargetPosition;
            @MousePosition.started -= instance.OnMousePosition;
            @MousePosition.performed -= instance.OnMousePosition;
            @MousePosition.canceled -= instance.OnMousePosition;
            @RotateBuilding.started -= instance.OnRotateBuilding;
            @RotateBuilding.performed -= instance.OnRotateBuilding;
            @RotateBuilding.canceled -= instance.OnRotateBuilding;
            @MouseDelta.started -= instance.OnMouseDelta;
            @MouseDelta.performed -= instance.OnMouseDelta;
            @MouseDelta.canceled -= instance.OnMouseDelta;
        }

        /// <summary>
        /// Unregisters <param cref="instance" /> and unregisters all input action callbacks via <see cref="CameraActions.UnregisterCallbacks(ICameraActions)" />.
        /// </summary>
        /// <seealso cref="CameraActions.UnregisterCallbacks(ICameraActions)" />
        public void RemoveCallbacks(ICameraActions instance)
        {
            if (m_Wrapper.m_CameraActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        /// <summary>
        /// Replaces all existing callback instances and previously registered input action callbacks associated with them with callbacks provided via <param cref="instance" />.
        /// </summary>
        /// <remarks>
        /// If <paramref name="instance" /> is <c>null</c>, calling this method will only unregister all existing callbacks but not register any new callbacks.
        /// </remarks>
        /// <seealso cref="CameraActions.AddCallbacks(ICameraActions)" />
        /// <seealso cref="CameraActions.RemoveCallbacks(ICameraActions)" />
        /// <seealso cref="CameraActions.UnregisterCallbacks(ICameraActions)" />
        public void SetCallbacks(ICameraActions instance)
        {
            foreach (var item in m_Wrapper.m_CameraActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_CameraActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    /// <summary>
    /// Provides a new <see cref="CameraActions" /> instance referencing this action map.
    /// </summary>
    public CameraActions @Camera => new CameraActions(this);
    /// <summary>
    /// Interface to implement callback methods for all input action callbacks associated with input actions defined by "Camera" which allows adding and removing callbacks.
    /// </summary>
    /// <seealso cref="CameraActions.AddCallbacks(ICameraActions)" />
    /// <seealso cref="CameraActions.RemoveCallbacks(ICameraActions)" />
    public interface ICameraActions
    {
        /// <summary>
        /// Method invoked when associated input action "MoveOnMiddleButton" is either <see cref="UnityEngine.InputSystem.InputAction.started" />, <see cref="UnityEngine.InputSystem.InputAction.performed" /> or <see cref="UnityEngine.InputSystem.InputAction.canceled" />.
        /// </summary>
        /// <seealso cref="UnityEngine.InputSystem.InputAction.started" />
        /// <seealso cref="UnityEngine.InputSystem.InputAction.performed" />
        /// <seealso cref="UnityEngine.InputSystem.InputAction.canceled" />
        void OnMoveOnMiddleButton(InputAction.CallbackContext context);
        /// <summary>
        /// Method invoked when associated input action "Move" is either <see cref="UnityEngine.InputSystem.InputAction.started" />, <see cref="UnityEngine.InputSystem.InputAction.performed" /> or <see cref="UnityEngine.InputSystem.InputAction.canceled" />.
        /// </summary>
        /// <seealso cref="UnityEngine.InputSystem.InputAction.started" />
        /// <seealso cref="UnityEngine.InputSystem.InputAction.performed" />
        /// <seealso cref="UnityEngine.InputSystem.InputAction.canceled" />
        void OnMove(InputAction.CallbackContext context);
        /// <summary>
        /// Method invoked when associated input action "Selection" is either <see cref="UnityEngine.InputSystem.InputAction.started" />, <see cref="UnityEngine.InputSystem.InputAction.performed" /> or <see cref="UnityEngine.InputSystem.InputAction.canceled" />.
        /// </summary>
        /// <seealso cref="UnityEngine.InputSystem.InputAction.started" />
        /// <seealso cref="UnityEngine.InputSystem.InputAction.performed" />
        /// <seealso cref="UnityEngine.InputSystem.InputAction.canceled" />
        void OnSelection(InputAction.CallbackContext context);
        /// <summary>
        /// Method invoked when associated input action "SetTargetPosition" is either <see cref="UnityEngine.InputSystem.InputAction.started" />, <see cref="UnityEngine.InputSystem.InputAction.performed" /> or <see cref="UnityEngine.InputSystem.InputAction.canceled" />.
        /// </summary>
        /// <seealso cref="UnityEngine.InputSystem.InputAction.started" />
        /// <seealso cref="UnityEngine.InputSystem.InputAction.performed" />
        /// <seealso cref="UnityEngine.InputSystem.InputAction.canceled" />
        void OnSetTargetPosition(InputAction.CallbackContext context);
        /// <summary>
        /// Method invoked when associated input action "MousePosition" is either <see cref="UnityEngine.InputSystem.InputAction.started" />, <see cref="UnityEngine.InputSystem.InputAction.performed" /> or <see cref="UnityEngine.InputSystem.InputAction.canceled" />.
        /// </summary>
        /// <seealso cref="UnityEngine.InputSystem.InputAction.started" />
        /// <seealso cref="UnityEngine.InputSystem.InputAction.performed" />
        /// <seealso cref="UnityEngine.InputSystem.InputAction.canceled" />
        void OnMousePosition(InputAction.CallbackContext context);
        /// <summary>
        /// Method invoked when associated input action "RotateBuilding" is either <see cref="UnityEngine.InputSystem.InputAction.started" />, <see cref="UnityEngine.InputSystem.InputAction.performed" /> or <see cref="UnityEngine.InputSystem.InputAction.canceled" />.
        /// </summary>
        /// <seealso cref="UnityEngine.InputSystem.InputAction.started" />
        /// <seealso cref="UnityEngine.InputSystem.InputAction.performed" />
        /// <seealso cref="UnityEngine.InputSystem.InputAction.canceled" />
        void OnRotateBuilding(InputAction.CallbackContext context);
        /// <summary>
        /// Method invoked when associated input action "MouseDelta" is either <see cref="UnityEngine.InputSystem.InputAction.started" />, <see cref="UnityEngine.InputSystem.InputAction.performed" /> or <see cref="UnityEngine.InputSystem.InputAction.canceled" />.
        /// </summary>
        /// <seealso cref="UnityEngine.InputSystem.InputAction.started" />
        /// <seealso cref="UnityEngine.InputSystem.InputAction.performed" />
        /// <seealso cref="UnityEngine.InputSystem.InputAction.canceled" />
        void OnMouseDelta(InputAction.CallbackContext context);
    }
}
