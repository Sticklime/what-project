using UnityEngine;

namespace CodeBase.Infrastructure.Bootstrapper.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly Contexts _context = Contexts.sharedInstance;

        public void CreateEntityCamera(Camera camera)
        {
            GameEntity cameraEntity = _context.game.CreateEntity();
            InputEntity cameraInputEntity = _context.input.CreateEntity();

            Transform cameraTransform = camera.GetComponent<Transform>();

            cameraEntity.AddModel(cameraTransform);
            cameraEntity.AddDirection(new Vector3(), 5);
            cameraInputEntity.AddCameraInputComponents(0,0);
        }
    }
}