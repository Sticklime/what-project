using CodeBase.Components;
using CodeBase.Components.InputContext.Camera;
using CodeBase.Data.StaticData;
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
                RaycastInputComponent raycast = _inputFilter.GetSingleEntity().raycastInput;
                ModelComponent buildingModel = entity.model;
                BuildingType buildingPlanType = entity.buildingPlan.BuildingType;

                if (raycast.IsSelection)
                {
                    _gameFactory.CreateBuilding(buildingModel.Transform.position, buildingPlanType);

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