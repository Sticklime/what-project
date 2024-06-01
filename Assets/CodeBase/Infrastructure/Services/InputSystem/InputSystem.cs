using UnityEngine;

namespace CodeBase.Infrastructure.Services.InputSystem
{
    public class InputSystem : IInitializationInput
    {
        private readonly PlayerInput _playerInput = new();

        public Vector3 DirectionAxis => _playerInput.Camera.Move.ReadValue<Vector3>();
        public Vector2 MousePosition => _playerInput.Camera.MousePosition.ReadValue<Vector2>();
        public bool LeftMouseButton => _playerInput.Camera.Selection.ReadValue<float>() != 0;
        public bool RightMouseButton => _playerInput.Camera.SetTargetPosition.ReadValue<float>() != 0;

        public void EnableSystem() =>
            _playerInput.Enable();
    }
}