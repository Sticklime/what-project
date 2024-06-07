using Entitas;

namespace CodeBase.EntitySystems.Build
{
    public class FollowRaycastSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _gameFilter;
        private readonly IGroup<InputEntity> _inputFilter;

        public FollowRaycastSystem(GameContext gameContext, InputContext inputContext)
        {
            _gameFilter = gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.Material, GameMatcher.Model));
            _inputFilter = inputContext.GetGroup(InputMatcher.RaycastInput);
        }

        public void Execute()
        {
            foreach (GameEntity gameEntity in _gameFilter)
            {
                var model = gameEntity.model;
                var raycast = _inputFilter.GetSingleEntity().raycastInput;

                model.Transform.position = raycast.TargetPosition;
            }
        }
    }
}