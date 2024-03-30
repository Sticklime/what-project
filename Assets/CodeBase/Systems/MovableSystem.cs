using CodeBase.Components;
using Components;
using Entitas;
using UnityEngine;

namespace CodeBase.Systems
{
    public class MovableSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _gameFilter;
        private readonly IGroup<InputEntity> _inputFilter;

        public MovableSystem(GameContext gameContext, InputContext inputContext)
        {
            _gameFilter = gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.Model, GameMatcher.Direction));
            _inputFilter = inputContext.GetGroup(InputMatcher.CameraInputComponents);
        }

        public void Execute()
        {
            foreach (var e in _gameFilter)
            {
                DirectionComponent direction = e.direction;
                Transform transform = e.model.Transform;

                InputEntity inputEntity = _inputFilter.GetSingleEntity();
                CameraInputComponents inputComponent = inputEntity.cameraInputComponents;

                SetDirection(direction, inputComponent);
                transform.position += NextPosition(direction);
            }
        }

        private Vector3 NextPosition(DirectionComponent direction) =>
            direction.Direction * direction.Speed * Time.deltaTime;

        private void SetDirection(DirectionComponent direction, CameraInputComponents inputComponent) =>
            direction.Direction = new Vector3(inputComponent.DirectionX, 0, inputComponent.DirectionZ);
    }
}