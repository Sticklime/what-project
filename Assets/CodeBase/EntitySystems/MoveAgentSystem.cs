using Entitas;

namespace CodeBase.EntitySystems
{
    public class MoveAgentSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _gameFilter;
        private readonly IGroup<InputEntity> _inputFilter;

        public MoveAgentSystem(GameContext gameContext, InputContext inputContext)
        {
            _gameFilter = gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.CharacterController, GameMatcher.SelectReceiver));
            _inputFilter = inputContext.GetGroup(InputMatcher.RaycastInput);
        }

        public void Execute()
        {
            foreach (GameEntity entity in _gameFilter)
            {
                var agent = entity.characterController;
                var selectReceiver = entity.selectReceiver;
                var targetPosition = _inputFilter.GetSingleEntity().raycastInput.TargetPosition;
                
                if (selectReceiver.IsSelect && agent.CanMove)
                    agent.CharacterController.destination = targetPosition;
            }
        }
    }
}