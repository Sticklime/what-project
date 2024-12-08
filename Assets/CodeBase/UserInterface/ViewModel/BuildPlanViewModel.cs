using CodeBase.Data.StaticData;
using CodeBase.Domain.BuildingSystem;
using CodeBase.Infrastructure.Services.ConfigProvider;
using Unity.Properties;
using UnityEngine;

namespace CodeBase.UserInterface.ViewModel
{
    public class BuildPlanViewModel
    {
        private readonly BuildingOperation _buildingOperation;
        private readonly BuildingConfig _buildingConfig;
        private readonly BuildingType _buildingType;

        [CreateProperty] public string NameButton { get; set; }

        public BuildPlanViewModel(BuildingOperation buildingOperation, IConfigProvider configProvider, BuildingType buildingType)
        {
            _buildingType = buildingType;
            _buildingConfig = configProvider.GetBuilding(_buildingType);
            NameButton = _buildingConfig.Name;
            _buildingOperation = buildingOperation;
        }

        public void CreateBuildPlan()
        {
            _buildingOperation.PurchaseBuilding(_buildingType, Vector3.zero);
        }
    }
}