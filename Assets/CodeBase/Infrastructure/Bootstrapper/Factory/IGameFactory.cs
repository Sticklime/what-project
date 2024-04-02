using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Bootstrapper.Factory
{
    public interface IGameFactory
    {
        Task Load();
        GameEntity CreateUnit(Vector3 at);
        GameEntity CreateEnemy(Vector3 at);
        void CreateEntityCamera(Camera camera);
    }
}