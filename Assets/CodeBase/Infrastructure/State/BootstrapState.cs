using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.AssetProvider;
using CodeBase.Infrastructure.Services.ConfigProvider;
using CodeBase.Infrastructure.Services.InputSystem;
using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.State
{
    public class BootstrapState : IState
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

            _gameStateMachine.Enter<BootSystemState>();
        }

        public void Exit()
        {
        }
    }
}