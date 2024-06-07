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
        private readonly BuildingData _buildingData;
        private readonly BuildingType _buildingType;

        [CreateProperty] public string NameButton { get; set; }

        public BuildPlanViewModel(BuildingOperation buildingOperation, IConfigProvider configProvider, BuildingType buildingType)
        {
            _buildingType = buildingType;
            _buildingData = configProvider.GetBuildingData(_buildingType);
            NameButton = _buildingData.Name;
            _buildingOperation = buildingOperation;
        }

        public void CreateBuildPlan()
        {
            _buildingOperation.PurchaseBuilding(_buildingType, Vector3.zero);
        }
    }
}