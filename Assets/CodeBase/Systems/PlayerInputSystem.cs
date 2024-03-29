using Entitas;
using UnityEngine;

namespace CodeBase.Systems
{
    public class PlayerInputSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _inputFilter;

        private float _directionX;
        private float _directionZ;

        public PlayerInputSystem()
        {
            _inputFilter =
                Contexts.sharedInstance.game.GetGroup(GameMatcher.AllOf(GameMatcher.ComponentsModel,
                    GameMatcher.ComponentsDirection));
        }

        public void Execute()
        {
            foreach (var e in _inputFilter)
            {
                SetDirection();

                e.componentsDirection.direction = new Vector3(_directionX, 0, _directionZ);
            }
        }

        private void SetDirection()
        {
            _directionX = Input.GetAxis("Horizontal");
            _directionZ = Input.GetAxis("Vertical");
        }
    }
}