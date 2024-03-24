using CodeBase.Infrastructure.Bootstrapper;
using CodeBase.Infrastructure.Services.SceneLoader;
using Zenject;

namespace CodeBase.Installer
{
    public class ServicesBind : MonoInstaller
    {
        public override void InstallBindings()
        {
            SceneLoaderBind();
            EntityFactoryBind();
        }

        private void SceneLoaderBind() =>
            Container
                .Bind<ISceneLoader>()
                .To<SceneLoader>()
                .AsSingle();

        private void EntityFactoryBind() =>
            Container
                .Bind<IGameFactory>()
                .To<GameFactory>()
                .AsSingle();
    }
}