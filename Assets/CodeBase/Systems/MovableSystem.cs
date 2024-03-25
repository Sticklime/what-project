using Components;
using Entitas;
using UnityEngine;

namespace CodeBase.Systems
{
    public class MovableSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _filter;

        public MovableSystem(Contexts contexts)
        {
            _filter = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.ComponentsModel,
                GameMatcher.ComponentsDirection));
        }

        public void Execute()
        {
            foreach (var e in _filter)
            {
                DirectionComponent direction = e.componentsDirection;
                Transform transform = e.componentsModel.Transform;

                transform.position = Vector3.MoveTowards(transform.position, direction.Direction,
                    direction.Speed * Time.deltaTime);
                
                Debug.Log("1Work");
            }
        }
    }
}