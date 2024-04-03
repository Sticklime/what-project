using CodeBase.Infrastructure.Bootstrapper.Factory;
using CodeBase.Infrastructure.Services.AssetManager;

namespace CodeBase.Infrastructure.Bootstrapper.State
{
    public class BootstrapState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IGameFactory _gameFactory;
        private readonly IAssetProvider _assetProvider;

        public BootstrapState(IGameStateMachine gameStateMachine, IGameFactory gameFactory,
            IAssetProvider assetProvider)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
            _assetProvider = assetProvider;
        }

        public async void Enter()
        {
            await _gameFactory.Load();
            _assetProvider.Initialize();

            _gameStateMachine.Enter<BootSystemState>();
        }

        public void Exit()
        {
        }
    }
}