using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory
    {
        UniTask Load();
        void CreateUnit(Vector3 at);
        void CreateEnemy(Vector3 at);
        void CreateBuildingPlan(Vector3 at);
        void CreateEntityCamera(Camera camera);
        void CreateBuilding(Vector3 at);
    }
}