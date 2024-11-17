using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Components.Building;
using CodeBase.Data.StaticData;
using CodeBase.Infrastructure.Services.AssetProvider;
using CodeBase.Infrastructure.Services.ConfigProvider;
using Cysharp.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.AddressableAssets;
using Zenject;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly DiContainer _diContainer;
        private readonly IAssetProvider _assetProvider;
        private readonly IConfigProvider _configProvider;
        private readonly Contexts _context;

        private GameObject _units;
        private GameObject _enemy;
        private GameObject _barracksPlan;

        public GameFactory(DiContainer diContainer, IAssetProvider assetProvider, IConfigProvider configProvider)
        {
            _diContainer = diContainer;
            _assetProvider = assetProvider;
            _configProvider = configProvider;
            _context = Contexts.sharedInstance;
        }

        public async UniTask Load()
        {

            _units = await _assetProvider.LoadAsync<GameObject>("Warrior");
            _enemy = await _assetProvider.LoadAsync<GameObject>("SpiderFugaBaby");
            _barracksPlan = await _assetProvider.LoadAsync<GameObject>("BarrackPlan");
        }

        public void CreateUnit(Vector3 at)
        {
            GameEntity characterEntity = _context.game.CreateEntity();
            InputEntity unitInputEntity = _context.input.CreateEntity();
            GameObject characterInstance = Object.Instantiate(_units, at, Quaternion.identity);

            NavMeshAgent characterController = characterInstance.GetComponent<NavMeshAgent>();
            BoxCollider selectReceiver = characterInstance.GetComponentInChildren<BoxCollider>();

            characterEntity.AddCharacterController(characterController, false);
            characterEntity.AddSelectReceiver(selectReceiver, false);
        }

        public async UniTask CreateBuildingPlan(Vector3 at, BuildingType buildingType)
        {
            BuildingData buildingData = _configProvider.GetBuildingData(buildingType);
            GameObject buildingPrefab = await _assetProvider.LoadAsync<GameObject>(buildingData.BuildingReference);

            GameEntity buildingPlanEntity = _context.game.CreateEntity();
            GameObject buildingPlanInstance = _diContainer.InstantiatePrefab(_barracksPlan);

            buildingPlanEntity.AddBuildingPlan(buildingType);
            buildingPlanEntity.AddModel(buildingPlanInstance.transform);
            buildingPlanEntity.AddMaterial(buildingPlanInstance.GetComponent<Material>());
        }

        public async UniTask CreateBuilding(Vector3 at, BuildingType buildingType)
        {
            BuildingData buildingData = _configProvider.GetBuildingData(buildingType);
            var buildingPrefab = await _assetProvider.LoadAsync<GameObject>(buildingData.BuildingReference);

            GameEntity buildEntity = _context.game.CreateEntity();
            GameObject buildInstance = Object.Instantiate(buildingPrefab, at, Quaternion.identity);

            buildEntity.AddBuilding(buildingType);
            buildEntity.AddModel(buildInstance.transform);
        }

        public void CreateEnemy(Vector3 at)
        {
            GameEntity enemyEntity = _context.game.CreateEntity();
            GameObject enemyInstance = Object.Instantiate(_enemy, at, Quaternion.identity);
        }

        public void CreateEntityCamera(Camera camera)
        {
            GameEntity cameraEntity = _context.game.CreateEntity();
            InputEntity cameraInputEntity = _context.input.CreateEntity();

            cameraEntity.AddCamera(camera.GetComponent<Camera>());
            cameraEntity.AddModel(camera.transform);
            cameraEntity.AddDirection(Vector3.zero, 5);

            cameraInputEntity.AddRaycastInput(Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, false);
            cameraInputEntity.AddCameraInputComponents(Vector3.zero);
        }
    }
}