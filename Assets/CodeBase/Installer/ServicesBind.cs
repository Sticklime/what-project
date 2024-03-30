using CodeBase.Infrastructure.Bootstrapper.Factory;
using CodeBase.Infrastructure.Services.SceneLoader;
using CodeBase.Infrastructure.Bootstrapper.State;
using CodeBase.Infrastructure.Bootstrapper;
using CodeBase.Infrastructure;
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
            BindInputContext();
            BindGameContext();
            BindSystemEngine();
        }

        private void BindGameContext() =>
            Container
                .Bind<GameContext>()
                .FromInstance(Contexts.sharedInstance.game)
                .AsSingle();

        private void BindInputContext() =>
            Container
                .Bind<InputContext>()
                .FromInstance(Contexts.sharedInstance.input)
                .AsSingle();

        private void BindSystemEngine() =>
            Container
                .Bind<SystemEngine>()
                .AsSingle();

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