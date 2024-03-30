namespace CodeBase.Infrastructure.Bootstrapper.State
{
    public class BootstrapState : IState
    {
        private readonly IGameStateMachine _stateMachine;

        public BootstrapState(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            _stateMachine.Enter<LoadMapState>();
        }

        public void Exit()
        {
            
        }
    }
}