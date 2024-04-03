using Entitas;
using UnityEngine;

namespace CodeBase.EntitySystems
{
    public class MouseInputSystem : IExecuteSystem
    {
        private readonly IGroup<InputEntity> _inputFilter;
        private readonly Transform _cameraTransform;

        private Ray _ray;
        private RaycastHit _raycastHit;

        public MouseInputSystem(InputContext inputContext)
        {
            _inputFilter = inputContext.GetGroup(InputMatcher.MouseInput);
        }

        public void Execute()
        {
            foreach (InputEntity entity in _inputFilter)
            {
                var mouseInput = entity.mouseInput;

                if (Input.GetMouseButton(0))
                {
                    if (Physics.Raycast(_ray, out _raycastHit, 100f, LayerMask.NameToLayer("Warfare"))) 
                        mouseInput.TargetPosition = _raycastHit.point;
                }
            }
        }
    }
}