using CodeBase.Infrastructure.Services.SceneLoader;
using Zenject;

namespace CodeBase.Zenject
{
    public class ServicesBind : MonoInstaller
    {
        public override void InstallBindings()
        {
            SceneLoaderBind.Bind<ISceneLoader>().To<SceneLoader>();
        }
    }
}