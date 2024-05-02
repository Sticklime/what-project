using CodeBase.Components;
using CodeBase.Components.InputContext;
using Entitas;
using UnityEngine;

namespace CodeBase.EntitySystems
{
    public class CameraMovableSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _gameFilter;
        private readonly IGroup<InputEntity> _inputFilter;

        public CameraMovableSystem(GameContext gameContext, InputContext inputContext)
        {
            _gameFilter = gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.Model, GameMatcher.Direction));
            _inputFilter = inputContext.GetGroup(InputMatcher.CameraInputComponents);
        }

        public void Execute()
        {
            foreach (var entity in _gameFilter)
            {
                DirectionComponent direction = entity.direction;
                Transform transform = entity.model.Transform;

                InputEntity inputEntity = _inputFilter.GetSingleEntity();
                CameraInputComponents inputComponent = inputEntity.cameraInputComponents;

                direction.Direction = inputComponent.MoveDirection;

                transform.position += direction.Direction * direction.Speed * Time.deltaTime;
            }
        }
    }
}