using CodeBase.Infrastructure.Bootstrapper;
using CodeBase.Infrastructure.Bootstrapper.Factory;
using CodeBase.Infrastructure.Bootstrapper.State;
using CodeBase.Infrastructure.Services.SceneLoader;
using Zenject;

namespace CodeBase.Installer
{
    public class ServicesBind : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindGameStateMachine();
            BindSceneLoader();
            BindEntityFactory();
            BindStateFactory();
        }

        private void BindGameStateMachine() =>
            Container
                .Bind<IGameStateMachine>()
                .To<StateMachine>()
                .AsSingle();

        private void BindStateFactory() =>
            Container
                .Bind<IStateFactory>()
                .To<StateFactory>()
                .AsSingle();

        private void BindSceneLoader() =>
            Container
                .Bind<ISceneLoader>()
                .To<SceneLoader>()
                .AsSingle();

        private void BindEntityFactory() =>
            Container
                .Bind<IGameFactory>()
                .To<GameFactory>()
                .AsSingle();
    }
}