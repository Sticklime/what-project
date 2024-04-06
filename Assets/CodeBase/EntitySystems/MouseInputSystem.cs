using CodeBase.Components.InputContext;
using UnityEngine;
using Entitas;

namespace CodeBase.EntitySystems
{
    public class MouseInputSystem : IExecuteSystem
    {
        private readonly IGroup<InputEntity> _inputFilter;
        private readonly IGroup<GameEntity> _cameraFilter;

        private RaycastHit _raycastHit;
        private Ray _ray;

        public MouseInputSystem(InputContext inputContext, GameContext gameContext)
        {
            _inputFilter = inputContext.GetGroup(InputMatcher.MouseInput);
            _cameraFilter = gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.Camera, GameMatcher.Model));
        }

        public void Execute()
        {
            foreach (InputEntity entity in _inputFilter)
            {
                MouseInputComponent mouseInput = entity.mouseInput;
                var cameraEntity = _cameraFilter.GetSingleEntity();
                var camera = cameraEntity.camera.Camera;

                _ray = camera.ScreenPointToRay(Input.mousePosition);

                if (Input.GetMouseButtonUp(1) && IsHitRaycast())
                    mouseInput.TargetPosition = _raycastHit.point;
                else if (Input.GetMouseButton(0))
                {
                    
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    
                }
            }
        }

        private bool IsHitRaycast() =>
            Physics.Raycast(_ray, out _raycastHit);
    }
}