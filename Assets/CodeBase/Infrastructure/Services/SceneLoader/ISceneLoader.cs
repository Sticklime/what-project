using System;
using System.Threading.Tasks;

namespace CodeBase.Infrastructure.Services.SceneLoader
{
    public interface ISceneLoader
    {
        Task Load(string name, Action onLoaded = null);
    }
}