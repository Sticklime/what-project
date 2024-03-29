using UnityEngine;

namespace CodeBase.Infrastructure.Bootstrapper
{
    public class GameFactory : IGameFactory
    {
        private readonly Contexts _context = Contexts.sharedInstance;

        public void CreateEntityCamera(Camera camera)
        {
            GameEntity cameraEntity = _context.game.CreateEntity();

            Transform cameraTransform = camera.GetComponent<Transform>();

            cameraEntity.AddComponentsModel(cameraTransform);
            cameraEntity.AddComponentsDirection(new Vector3(), 5);
        }
    }
}