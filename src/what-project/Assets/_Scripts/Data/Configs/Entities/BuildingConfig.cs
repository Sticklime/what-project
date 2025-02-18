using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Data.StaticData
{
    [CreateAssetMenu(menuName = "StaticData/BuildingData", fileName = "NewBuildingData")]
    public class BuildingConfig : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject BuildingReference { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject BuildingPlanReference { get; private set; }
        [field: SerializeField] public BuildingType BuildingType { get; private set; }
        [field: SerializeField] public List<ResourcesStaticData> RequiredResources { get; private set; }
    }

    public enum BuildingType
    {
        Default = 0,
        Barrack = 1,
    }
}