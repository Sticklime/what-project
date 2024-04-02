using CodeBase.Infrastructure.Bootstrapper.Factory;

namespace CodeBase.Infrastructure.Bootstrapper.State
{
    public class BootstrapState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IGameFactory _gameFactory;

        public BootstrapState(IGameStateMachine gameStateMachine,IGameFactory gameFactory)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
        }

        public async void Enter()
        {
            await _gameFactory.Load();
            
            _gameStateMachine.Enter<BootSystemState>();
        }

        public void Exit()
        {
        }
    }
}