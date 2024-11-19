using System;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.NetCode;
using CodeBase.Infrastructure.Services.AssetProvider;
using CodeBase.Infrastructure.Services.ConfigProvider;
using CodeBase.Infrastructure.Services.InputSystem;
using Fusion;

namespace CodeBase.Infrastructure.State
{
    public class BootstrapState : IState, IDisposable
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IGameFactory _gameFactory;
        private readonly IAssetProvider _assetProvider;
        private readonly IInputSystem _inputSystem;
        private readonly IUIFactory _uiFactory;
        private readonly IConfigProvider _configProvider;
        
        public BootstrapState(IGameStateMachine gameStateMachine, IGameFactory gameFactory,
            IAssetProvider assetProvider, IInputSystem inputSystem, IUIFactory uiFactory,
            IConfigProvider configProvider)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
            _assetProvider = assetProvider;
            _inputSystem = inputSystem;
            _uiFactory = uiFactory;
            _configProvider = configProvider;
        }

        public async void Enter()
        {
            await _assetProvider.InitializeAsset();
            await _configProvider.Load();
            await _gameFactory.Load();
            await _uiFactory.Load();

            if (_inputSystem is IInitializationInput inputSystem)
                inputSystem.EnableSystem();
            
            ConnectManager.Instance.OnPlayerJoin += Bootstrap;
            
            await ConnectManager.Instance.Join();
        }

        private void Bootstrap(PlayerRef playerRef)
        {
            ConnectManager.Instance.OnPlayerJoin -= Bootstrap;
            _gameStateMachine.Enter<BootSystemState>(playerRef);
        }

        public void Exit()
        {
        }

        public void Dispose() => 
            ConnectManager.Instance.OnPlayerJoin -= Bootstrap;
    }
}