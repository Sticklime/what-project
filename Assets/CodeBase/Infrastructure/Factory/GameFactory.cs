using CodeBase.Infrastructure.Services.AssetManager;
using CodeBase.Infrastructure.Bootstrapper.Factory;
using System.Threading.Tasks;
using CodeBase.Components.InputContext;
using UnityEngine.AI;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly DiContainer _diContainer;
        private readonly IAssetProvider _assetProvider;
        private readonly Contexts _context;

        private GameObject _units;
        private GameObject _enemy;

        public GameFactory(DiContainer diContainer, IAssetProvider assetProvider)
        {
            _diContainer = diContainer;
            _assetProvider = assetProvider;
            _context = Contexts.sharedInstance;
        }

        public async Task Load()
        {
            _units = await _assetProvider.LoadAsync<GameObject>("Warrior");
            _enemy = await _assetProvider.LoadAsync<GameObject>("SpiderFugaBaby");
        }

        public GameEntity CreateUnit(Vector3 at)
        {
            GameEntity characterEntity = _context.game.CreateEntity();
            InputEntity unitInputEntity = _context.input.CreateEntity();
            GameObject characterInstance = _diContainer.InstantiatePrefab(_units);

            NavMeshAgent characterController = characterInstance.GetComponent<NavMeshAgent>();
            BoxCollider selectReceiver = characterInstance.GetComponentInChildren<BoxCollider>();

            characterEntity.AddCharacterController(characterController);
            characterEntity.AddSelectReceiver(selectReceiver, false);

            return characterEntity;
        }

        public GameEntity CreateEnemy(Vector3 at)
        {
            GameEntity enemyEntity = _context.game.CreateEntity();
            GameObject enemyInstance = _diContainer.InstantiatePrefab(_enemy);

            return enemyEntity;
        }

        public void CreateEntityCamera(Camera camera)
        {
            GameEntity cameraEntity = _context.game.CreateEntity();
            InputEntity cameraInputEntity = _context.input.CreateEntity();

            var cameraTransform = camera.GetComponent<Transform>();

            cameraEntity.AddCamera(cameraTransform.GetComponent<Camera>());
            cameraEntity.AddModel(cameraTransform);
            cameraEntity.AddDirection(new Vector3(), 5);

            cameraInputEntity.AddRaycastInput(Vector3.zero, Vector3.zero, Vector3.zero, false);
            cameraInputEntity.AddCameraInputComponents(0, 0);
        }
    }
}