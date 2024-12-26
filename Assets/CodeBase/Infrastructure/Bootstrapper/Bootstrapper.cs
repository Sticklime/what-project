using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.State;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Infrastructure.Bootstrapper
{
    public class Bootstrapper : IInitializable
    {
        private IGameStateMachine _stateMachine;
        private IStateFactory _stateFactory;

        [Inject]
        private void Construct(IGameStateMachine stateMachine, IStateFactory stateFactory)
        {
            _stateMachine = stateMachine;
            _stateFactory = stateFactory;
        }

        public void Initialize()
        {
            RegisterState();
            
            
            _stateMachine.Enter<BootstrapState>();
        }

        private void RegisterState()
        {
            _stateMachine.RegisterState<BootstrapState>(_stateFactory.CreateSystem<BootstrapState>());
            _stateMachine.RegisterState<BootSystemState>(_stateFactory.CreateSystem<BootSystemState>());
            _stateMachine.RegisterState<LoadSaveState>(_stateFactory.CreateSystem<LoadSaveState>());
            _stateMachine.RegisterState<StartServerState>(_stateFactory.CreateSystem<StartServerState>());
            _stateMachine.RegisterState<ConnectToServer>(_stateFactory.CreateSystem<ConnectToServer>());
            _stateMachine.RegisterState<LoadMapState>(_stateFactory.CreateSystem<LoadMapState>());
        }
    }
}