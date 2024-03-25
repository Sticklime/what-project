using Entitas;
using UnityEngine;

namespace CodeBase.Systems
{
    public class PlayerInputSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _inputFilter;

        private float _directionX;
        private float _directionY;

        public PlayerInputSystem(Contexts contexts)
        {
            _inputFilter = contexts.game.GetGroup(GameMatcher.ComponentsDirection);
        }

        public void Execute()
        {
            SetDirection();

            foreach (var i in _inputFilter)
            {
                i.componentsDirection.Direction = new Vector3(_directionX, _directionY);
                
                Debug.Log("Work");
            }
        }

        private void SetDirection()
        {
            _directionX = Input.GetAxis("Horizontal");
            _directionY = Input.GetAxis("Vertical");
        }
    }
}