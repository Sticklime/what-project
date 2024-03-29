using Components;
using Entitas;
using UnityEngine;

namespace CodeBase.Systems
{
    public class MovableSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _filter;

        public MovableSystem()
        {
            _filter = Contexts.sharedInstance.game.GetGroup(GameMatcher.AllOf(GameMatcher.ComponentsModel,
                GameMatcher.ComponentsDirection));
        }

        public void Execute()
        {
            foreach (var e in _filter)
            {
                DirectionComponent direction = e.componentsDirection;
                Transform transform = e.componentsModel.Transform;

                transform.position += direction.direction * direction.Speed * Time.deltaTime;
            }
        }
    }
}