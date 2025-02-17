using UnityEngine;

namespace CodeBase.Infrastructure.Services.InputSystem
{
    public class InputSystem : IInitializationInput
    {
        private readonly PlayerInput _playerInput;

        public Vector3 DirectionAxis => GetDirection();
        public Vector2 MousePosition => _playerInput.Camera.MousePosition.ReadValue<Vector2>();
        public bool LeftMouseButton => _playerInput.Camera.Selection.ReadValue<float>() != 0;
        public bool RightMouseButton => _playerInput.Camera.SetTargetPosition.ReadValue<float>() != 0;
        public bool RotationBuilding => _playerInput.Camera.RotateBuilding.ReadValue<float>() != 0;

        public InputSystem()
        {
            _playerInput = new();
        }

        private Vector3 GetDirection()
        {
            Vector3 moveDirection = MoveDirectionButton();

            if (moveDirection == Vector3.zero)
                moveDirection = MoveDirectionDelta();

            return moveDirection;
        }

        private Vector3 MoveDirectionButton() =>
            _playerInput.Camera.Move.ReadValue<Vector3>() != Vector3.zero
                ? _playerInput.Camera.Move.ReadValue<Vector3>()
                : Vector3.zero;

        private Vector3 MoveDirectionDelta() =>
            _playerInput.Camera.MoveOnMiddleButton.ReadValue<float>() != 0
                ? GetMouseDelta()
                : Vector3.zero;

        private Vector3 GetMouseDelta()
        {
            Vector2 mouseDelta = _playerInput.Camera.MouseDelta.ReadValue<Vector2>();

            return new Vector3(-mouseDelta.x, 0, -mouseDelta.y);
        }

        public void EnableSystem() =>
            _playerInput.Enable();
    }
}