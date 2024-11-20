using Entitas;

namespace CodeBase.EntitySystems.Building
{
    public class RotateBuildingSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _gameContext;
        private readonly IGroup<InputEntity> _inputContext;

        public RotateBuildingSystem(GameContext gameContext, InputContext inputContext)
        {
            _gameContext = gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.BuildingPlan, GameMatcher.Model));
            _inputContext = inputContext.GetGroup();
        }

        public void Execute()
        {
            foreach (GameEntity buildingPlan in _gameContext)

            {
            }
        }
    }
}