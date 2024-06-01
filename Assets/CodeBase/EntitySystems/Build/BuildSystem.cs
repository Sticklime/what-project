using CodeBase.Components;
using CodeBase.Infrastructure.Factory;
using Entitas;
using UnityEngine;

namespace CodeBase.EntitySystems.Build
{
    public class BuildSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _buildPlanFilter;
        private readonly IGroup<InputEntity> _inputFilter;
        private readonly IGameFactory _gameFactory;

        public BuildSystem(InputContext inputContext, GameContext gameContext, IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
            _buildPlanFilter = gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.BuildingPlan, GameMatcher.Material));
            _inputFilter = inputContext.GetGroup(InputMatcher.RaycastInput);
        }

        public void Execute()
        {
            foreach (var entity in _buildPlanFilter)
            {
                var raycast = _inputFilter.GetSingleEntity().raycastInput;
                var buildingModel = entity.model;

                if (raycast.IsSelection)
                {
                    _gameFactory.CreateBuilding(buildingModel.Transform.position);

                    DestroyBuildingPlan(buildingModel, entity);
                    return;
                }
            }
        }

        private void DestroyBuildingPlan(ModelComponent buildingModel, GameEntity entity)
        {
            entity.Destroy();
            Object.Destroy(buildingModel.Transform.gameObject);
        }
    }
}