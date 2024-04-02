using Entitas;
using UnityEngine;

namespace CodeBase.EntitasSystems
{
    public class PlayerInputSystem : IExecuteSystem
    {
        private readonly IGroup<InputEntity> _inputFilter;

        private float _directionX;
        private float _directionZ;

        public PlayerInputSystem(InputContext context)
        {
            _inputFilter = context.GetGroup(InputMatcher.CameraInputComponents);
        }

        public void Execute()
        {
            foreach (var e in _inputFilter)
            {
                SetDirection();

                e.cameraInputComponents.DirectionX = _directionX;
                e.cameraInputComponents.DirectionZ = _directionZ;
            }
        }

        private void SetDirection()
        {
            _directionX = Input.GetAxis("Horizontal");
            _directionZ = Input.GetAxis("Vertical");
        }
    }
}