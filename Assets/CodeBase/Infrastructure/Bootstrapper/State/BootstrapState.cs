using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.AssetManager;
using CodeBase.Infrastructure.Services.InputSystem;

namespace CodeBase.Infrastructure.Bootstrapper.State
{
    public class BootstrapState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IGameFactory _gameFactory;
        private readonly IAssetProvider _assetProvider;
        private readonly IInputSystem _inputSystem;
        private readonly IUIFactory _uiFactory;

        public BootstrapState(IGameStateMachine gameStateMachine, IGameFactory gameFactory,
            IAssetProvider assetProvider, IInputSystem inputSystem, IUIFactory uiFactory)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
            _assetProvider = assetProvider;
            _inputSystem = inputSystem;
            _uiFactory = uiFactory;
        }

        public async void Enter()
        {
            _assetProvider.LoadAssets();
            
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