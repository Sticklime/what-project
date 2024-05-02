using CodeBase.Infrastructure.Bootstrapper.Factory;
using CodeBase.Infrastructure.Services.SceneLoader;
using CodeBase.Infrastructure.Bootstrapper.State;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.AssetManager;
using CodeBase.Infrastructure.Services.AssetProvider;
using CodeBase.Infrastructure.Services.InputSystem;
using Zenject;

namespace CodeBase.Installer
{
    public class ServicesBind : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindInputSystem();
            BindGameStateMachine();
            BindAssetProvider();
            BindSceneLoader();
            BindGameFactory();
            BindUIFactory();
            BindStateFactory();
            BindInputContext();
            BindGameContext();
            BindSystemEngine();
        }

        private void BindUIFactory() =>
            Container
                .Bind<IUIFactory>()
                .To<UIFactory>()
                .AsSingle();

        private void BindInputSystem() =>
            Container
                .Bind<IInputSystem>()
                .To<InputSystem>()
                .AsSingle();

        private void BindAssetProvider() =>
            Container
                .Bind<IAssetProvider>()
                .To<AssetProvider>()
                .AsSingle();

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

        private void BindGameFactory() =>
            Container
                .Bind<IGameFactory>()
                .To<GameFactory>()
                .AsSingle();
    }
}