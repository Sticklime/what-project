using CodeBase.Data.StaticData;
using CodeBase.Domain.BuySystem;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.ConfigProvider;
using UnityEngine;

namespace CodeBase.Domain.BuildingSystem
{
    public class BuildingOperation
    {
        private readonly IConfigProvider _configProvider;
        private readonly IGameFactory _gameFactory;
        private readonly ResourcesOperation _resourcesOperation;

        public BuildingOperation(IConfigProvider configProvider, IGameFactory gameFactory,
            ResourcesOperation resourcesOperation)
        {
            _configProvider = configProvider;
            _gameFactory = gameFactory;
            _resourcesOperation = resourcesOperation;
        }

        public void PurchaseBuilding(BuildingType buildingType, Vector3 at)
        {
            BuildingData buildingData = _configProvider.GetBuildingData(buildingType);

            if (_resourcesOperation.TryPurchaseWithResource(buildingData.RequiredResources.ToArray()))
            {
                _gameFactory.CreateBuildingPlan(at, buildingType);
            }
        }
    }
}