using CodeBase.Data.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory
    {
        UniTask Load();
        void CreateUnit(Vector3 at);
        void CreateEnemy(Vector3 at);
        UniTask CreateBuildingPlan(Vector3 at, BuildingType buildingType);
        void CreateEntityCamera(Camera camera);
        UniTask CreateBuilding(Vector3 at, BuildingType buildingType);
    }
}