using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Bootstrapper
{
    public class GameFactory : IGameFactory
    {
        private Contexts _context;

        [Inject]
        public void Construct()
        {
            _context = Contexts.sharedInstance;
        }

        public void CreateEntityCamera(Camera camera)
        {
            GameEntity cameraEntity = _context.game.CreateEntity();

            cameraEntity.AddCodeBaseComponentsPosition(camera.GetComponent<Transform>());
        }
    }
}