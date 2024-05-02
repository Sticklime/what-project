using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory
    {
        Task Load();
        void CreateUnit(Vector3 at);
        void CreateEnemy(Vector3 at);
        void CreateEntityCamera(Camera camera);
    }
}