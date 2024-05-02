using CodeBase.Components;
using CodeBase.Components.InputContext;
using CodeBase.Infrastructure.Services.InputSystem;
using Entitas;
using UnityEngine;

namespace CodeBase.EntitySystems
{
    public class CameraInputSystem : IExecuteSystem
    {
        private readonly IInputSystem _inputSystem;
        private readonly IGroup<InputEntity> _inputFilter;
        private readonly IGroup<GameEntity> _moveFilter;

        private Vector2 _directionMove;

        public CameraInputSystem(InputContext inputContext, GameContext gameContext, IInputSystem inputSystem)
        {
            _inputSystem = inputSystem;
            _inputFilter =
                inputContext.GetGroup(InputMatcher.AllOf(InputMatcher.CameraInputComponents,
                    InputMatcher.RaycastInput));
            _moveFilter = gameContext.GetGroup(GameMatcher.CharacterController);
        }

        public void Execute()
        {
            foreach (var entity in _moveFilter)
            {
                var cameraEntity = _inputFilter.GetSingleEntity();
                var raycastInput = _inputFilter.GetSingleEntity().raycastInput;
                var characterController = entity.characterController;

                SetCameraDirection(cameraEntity);
                SetMousePosition(cameraEntity);
                TryMove(characterController);

                TrySelection(raycastInput);
            }
        }

        private void TrySelection(RaycastInputComponent raycastInput)
        {
            if (_inputSystem.IsSelection)
            {
                if (!raycastInput.IsSelection)
                    raycastInput.StartPositionSelection = raycastInput.TargetPosition;

                raycastInput.IsSelection = _inputSystem.IsSelection;
                raycastInput.EndPositionSelection = raycastInput.TargetPosition;
            }
            else
                raycastInput.IsSelection = _inputSystem.IsSelection;
        }

        private void TryMove(CharacterControllerComponent characterController) =>
            characterController.CanMove = _inputSystem.IsSetTarget;

        private void SetMousePosition(InputEntity cameraEntity) =>
            cameraEntity.raycastInput.MousePosition = _inputSystem.MousePosition;

        private void SetCameraDirection(InputEntity cameraEntity) =>
            cameraEntity.cameraInputComponents.MoveDirection = _inputSystem.DirectionAxis;
    }
}