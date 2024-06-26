using CodeBase.Infrastructure.Services.ConfigProvider;
using Entitas;
using UnityEngine;

namespace CodeBase.EntitySystems.Building
{
    public class GridSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _gameContext;
        private readonly IGroup<InputEntity> _inputContext;

        public GridSystem(GameContext gameContext, InputContext inputContext)
        {
            _gameContext = gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.BuildingPlan, GameMatcher.Model));
            _inputContext = inputContext.GetGroup(InputMatcher.RaycastInput);
        }

        public void Execute()
        {
            int buildingPositionX;
            int buildingPositionZ;

            foreach (GameEntity buildingPlan in _gameContext)
            {
                Transform transformPlan = buildingPlan.model.Transform;
                Vector3 position = transformPlan.position;

                buildingPositionX = Mathf.RoundToInt(position.x);
                buildingPositionZ = Mathf.RoundToInt(position.z);

                position = new Vector3(buildingPositionX, position.y, buildingPositionZ);
                transformPlan.position = position;
            }
        }
    }
}