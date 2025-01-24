using _Scripts.Netcore.Data.NetworkObjects;
using _Scripts.Netcore.FormatterSystem;
using _Scripts.Netcore.Initializer;
using _Scripts.Netcore.NetworkComponents.NetworkVariableComponent.Processor;
using _Scripts.Netcore.RPCSystem;
using _Scripts.Netcore.RPCSystem.Callers;
using _Scripts.Netcore.RPCSystem.DynamicProcessor;
using _Scripts.Netcore.RPCSystem.Processors;
using _Scripts.Netcore.Runner;
using _Scripts.Netcore.Spawner;
using CodeBase.Data;
using CodeBase.Domain.BuildingSystem;
using CodeBase.Domain.BuySystem;
using CodeBase.Infrastructure.Services.SceneLoader;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Bootstrapper;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.AssetProvider;
using CodeBase.Infrastructure.Services.ConfigProvider;
using CodeBase.Infrastructure.Services.InputSystem;
using CodeBase.Infrastructure.State;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Installer
{
    public class ServicesInstaller : LifetimeScope
    {
        [SerializeField] private NetworkObjectsConfig _networkObjectsConfig;
        
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterEntryPoint(builder);
            RegisterInputSystem(builder);
            RegisterSystem(builder);
            RegisterGameStateMachine(builder);
            RegisterAssetProvider(builder);
            RegisterSceneLoader(builder);
            RegisterStateFactory(builder);
            RegisterGameFactory(builder);
            RegisterUIFactory(builder);
            RegisterInputContext(builder);
            RegisterGameContext(builder);
            RegisterSystemEngine(builder);
            RegisterPersistentProgress(builder);
            RegisterConfigProvider(builder);
            RegisterResourcesOperation(builder);
            RegisterBuildingOperation(builder);
            builder.Register<INetworkRunner, NetworkRunner>(Lifetime.Singleton);
            builder.Register<INetworkFormatter, NetworkFormatter>(Lifetime.Singleton);
            builder.Register<IRpcListener, RPCListener>(Lifetime.Singleton);
            builder.Register<ICallerService, CallerService>(Lifetime.Singleton);
            builder.Register<IRPCReceiveProcessor, RPCReceiveReceiveProcessor>(Lifetime.Singleton);
            builder.Register<IRPCSendProcessor, RPCSendProcessor>(Lifetime.Singleton);
            builder.Register<IDynamicProcessorService, DynamicProcessorService>(Lifetime.Singleton);
            builder.Register<INetworkInitializer, NetworkInitializer>(Lifetime.Singleton);
            builder.Register<INetworkSpawner, NetworkSpawner>(Lifetime.Singleton).WithParameter(_networkObjectsConfig);

        }

        private void RegisterEntryPoint(IContainerBuilder builder) =>
            builder.RegisterEntryPoint<Bootstrapper>().AsSelf();

        private void RegisterSystem(IContainerBuilder builder) =>
            builder.Register<ISystemFactory, SystemFactory>(Lifetime.Singleton);

        private void RegisterBuildingOperation(IContainerBuilder builder) =>
            builder.Register<BuildingOperation>(Lifetime.Singleton);

        private void RegisterResourcesOperation(IContainerBuilder builder) =>
            builder.Register<ResourcesOperation>(Lifetime.Singleton);

        private void RegisterConfigProvider(IContainerBuilder builder) =>
            builder.Register<IConfigProvider, ConfigProvider>(Lifetime.Singleton);

        private void RegisterPersistentProgress(IContainerBuilder builder) =>
            builder.Register<IPersistentProgress, PersistentProgress>(Lifetime.Singleton);

        private void RegisterUIFactory(IContainerBuilder builder) =>
            builder.Register<IUIFactory, UIFactory>(Lifetime.Singleton);

        private void RegisterInputSystem(IContainerBuilder builder) =>
            builder.Register<IInputSystem, InputSystem>(Lifetime.Singleton);

        private void RegisterAssetProvider(IContainerBuilder builder) =>
            builder.Register<IAssetProvider, AssetProvider>(Lifetime.Singleton);

        private void RegisterGameContext(IContainerBuilder builder) =>
            builder.RegisterInstance(Contexts.sharedInstance.game);

        private void RegisterInputContext(IContainerBuilder builder) =>
            builder.RegisterInstance(Contexts.sharedInstance.input);

        private void RegisterSystemEngine(IContainerBuilder builder) =>
            builder.Register<SystemEngine>(Lifetime.Singleton);

        private void RegisterGameStateMachine(IContainerBuilder builder) =>
            builder.Register<IGameStateMachine, StateMachine>(Lifetime.Singleton);

        private void RegisterStateFactory(IContainerBuilder builder) =>
            builder.Register<IStateFactory, StateFactory>(Lifetime.Singleton);

        private void RegisterSceneLoader(IContainerBuilder builder) =>
            builder.Register<ISceneLoader, SceneLoader>(Lifetime.Singleton);

        private void RegisterGameFactory(IContainerBuilder builder) =>
            builder.Register<IGameFactory, GameFactory>(Lifetime.Singleton);
    }
}