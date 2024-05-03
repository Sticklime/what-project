using CodeBase.Infrastructure.Bootstrapper.Factory;
using CodeBase.Infrastructure.Services.SceneLoader;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.AssetManager;
using CodeBase.Infrastructure.Services.AssetProvider;
using CodeBase.Infrastructure.Services.InputSystem;
using CodeBase.Infrastructure.Services.PresenterLocator;
using CodeBase.Infrastructure.State;
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
            BindStateFactory();
            BindGameFactory();
            BindUIFactory();
            BindIPresenter();
            BindInputContext();
            BindGameContext();
            BindSystemEngine();
        }

        private void BindIPresenter()
        {
            Container
                .Bind<IPresenterLocator>()
                .To<PresenterLocator>()
                .AsSingle();
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