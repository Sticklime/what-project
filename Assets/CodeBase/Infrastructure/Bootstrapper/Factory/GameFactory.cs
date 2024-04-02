using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeBase.Infrastructure.Services.AssetManager;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Bootstrapper.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly DiContainer _diContainer;
        private readonly IAssetProvider _assetProvider;
        private readonly Contexts _context;

        private List<GameObject> _units;
        private List<GameObject> _enemy;

        public GameFactory(DiContainer diContainer, IAssetProvider assetProvider)
        {
            _diContainer = diContainer;
            _assetProvider = assetProvider;
            _context = Contexts.sharedInstance;
        }

        public async Task Load()
        {
            _units = await _assetProvider.LoadGroup("Units");
            _enemy = await _assetProvider.LoadGroup("Enemy");
        }

        public GameEntity CreateUnit(Vector3 at)
        {
            GameEntity characterEntity = _context.game.CreateEntity();
            GameObject characterInstance = _diContainer.InstantiatePrefab(_units.First());

            var characterController = characterInstance.GetComponent<CharacterController>();

            characterEntity.AddCharacterController(characterController);

            return characterEntity;
        }

        public GameEntity CreateEnemy(Vector3 at)
        {
            GameEntity enemyEntity = _context.game.CreateEntity();
            GameObject enemyInstance = _diContainer.InstantiatePrefab(_enemy.First());

            return enemyEntity;
        }

        public void CreateEntityCamera(Camera camera)
        {
            GameEntity cameraEntity = _context.game.CreateEntity();
            InputEntity cameraInputEntity = _context.input.CreateEntity();

            var cameraTransform = camera.GetComponent<Transform>();

            cameraEntity.AddModel(cameraTransform);
            cameraEntity.AddDirection(new Vector3(), 5);
            cameraInputEntity.AddCameraInputComponents(0, 0);
        }
    }
}