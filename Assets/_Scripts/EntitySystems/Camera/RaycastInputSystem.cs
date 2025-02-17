using Entitas;
using UnityEngine;
using Gizmos = Popcron.Gizmos;

namespace CodeBase.EntitySystems.Camera
{
    public class RaycastInputSystem : IExecuteSystem
    {
        private readonly IGroup<InputEntity> _inputFilter;
        private readonly IGroup<GameEntity> _cameraFilter;

        private LayerMask _layerMask = LayerMask.GetMask("Warfare");
        private RaycastHit _raycastHit;
        private Ray _ray;

        public RaycastInputSystem(InputContext inputContext, GameContext gameContext)
        {
            _inputFilter = inputContext.GetGroup(InputMatcher.RaycastInput);
            _cameraFilter = gameContext.GetGroup(GameMatcher.Camera);
        }

        public void Execute()
        {
            foreach (InputEntity inputEntity in _inputFilter)
            {
                var cameraEntity = _cameraFilter.GetSingleEntity();
                var camera = cameraEntity.camera.Camera;

                _ray = camera.ScreenPointToRay(inputEntity.raycastInput.MousePosition);

                if (IsHitRaycast())
                {
                    inputEntity.raycastInput.TargetPosition = _raycastHit.point;
                    DrawGizmo();
                }
            }
        }

        private void DrawGizmo()
        {
            Debug.DrawLine(_ray.origin, _raycastHit.point, Color.yellow);
            Gizmos.Sphere(_raycastHit.point, 0.1f, Color.red, true);
        }

        private bool IsHitRaycast() =>
            Physics.Raycast(_ray, out _raycastHit, _layerMask);
    }
}