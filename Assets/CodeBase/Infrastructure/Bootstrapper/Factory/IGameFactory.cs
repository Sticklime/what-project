using UnityEngine;

namespace CodeBase.Infrastructure.Bootstrapper
{
    public interface IGameFactory
    {
        void CreateEntityCamera(Camera camera);
    }
}