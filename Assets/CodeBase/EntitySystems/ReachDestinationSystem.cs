using Entitas;
using UnityEngine;

namespace CodeBase.EntitySystems
{
    public class ReachDestinationSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _gameFilter;
        private readonly IGroup<InputEntity> _inputFilter;

        public ReachDestinationSystem(GameContext gameContext, InputContext inputContext)
        {
            _gameFilter = gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.CharacterController));
            _inputFilter = inputContext.GetGroup(InputMatcher.MouseInput);
        }

        public void Execute()
        {
            foreach (GameEntity entity in _gameFilter)
            {
                var agent = entity.characterController.CharacterController;
                var targetPosition = _inputFilter.GetSingleEntity().mouseInput.TargetPosition;

                agent.destination = targetPosition;
            }
        }
    }
}