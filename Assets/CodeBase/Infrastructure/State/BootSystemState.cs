using CodeBase.Infrastructure.Factory;

namespace CodeBase.Infrastructure.State
{
    public class BootSystemState : IState
    {
        private readonly SystemEngine _systemEngine;
        private readonly ISystemFactory _systemFactory;
        private readonly IGameStateMachine _stateMachine;

        public BootSystemState(ISystemFactory systemFactory, SystemEngine systemEngine, IGameStateMachine stateMachine)
        {
            _systemFactory = systemFactory;
            _systemEngine = systemEngine;
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            _systemEngine.RegisterSystem(_systemFactory.CreateSystem<BuildFeature>());
            _systemEngine.RegisterSystem(_systemFactory.CreateSystem<InputFeature>());
            _systemEngine.RegisterSystem(_systemFactory.CreateSystem<CameraFeature>());
            _systemEngine.RegisterSystem(_systemFactory.CreateSystem<UnitFeature>());

            _systemEngine.StartSystem();

            _stateMachine.Enter<LoadSaveState>();
        }

        public void Exit()
        {
        }
    }
}