using CodeBase.Infrastructure.Services.AssetManager;
using System.Threading.Tasks;
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

        public void CreateUnit(Vector3 at)
        {
            GameEntity characterEntity = _context.game.CreateEntity();
            InputEntity unitInputEntity = _context.input.CreateEntity();
            GameObject characterInstance = _diContainer.InstantiatePrefab(_units);

            NavMeshAgent characterController = characterInstance.GetComponent<NavMeshAgent>();
            BoxCollider selectReceiver = characterInstance.GetComponentInChildren<BoxCollider>();

            characterEntity.AddCharacterController(characterController, false);
            characterEntity.AddSelectReceiver(selectReceiver, false);
        }

        public void CreateEnemy(Vector3 at)
        {
            GameEntity enemyEntity = _context.game.CreateEntity();
            GameObject enemyInstance = _diContainer.InstantiatePrefab(_enemy);
        }

        public void CreateEntityCamera(Camera camera)
        {
            GameEntity cameraEntity = _context.game.CreateEntity();
            InputEntity cameraInputEntity = _context.input.CreateEntity();

            var cameraTransform = camera.GetComponent<Transform>();

            cameraEntity.AddCamera(cameraTransform.GetComponent<Camera>());
            cameraEntity.AddModel(cameraTransform);
            cameraEntity.AddDirection(Vector3.zero, 5);

            cameraInputEntity.AddRaycastInput(Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, false);
            cameraInputEntity.AddCameraInputComponents(Vector3.zero);
        }
    }
}