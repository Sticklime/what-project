using System;

namespace CodeBase.Infrastructure.Services.SceneLoader
{
    public interface ISceneLoader
    {
        void Load(string name, Action onLoaded = null);
    }
}