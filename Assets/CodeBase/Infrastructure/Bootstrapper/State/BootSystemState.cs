using CodeBase.EntitySystems;

namespace CodeBase.Infrastructure.Bootstrapper.State
{
    public class BootSystemState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly SystemEngine _systemEngine;
        private readonly GameContext _gameContext;
        private readonly InputContext _inputContext;

        public BootSystemState(IGameStateMachine stateMachine, SystemEngine systemEngine, GameContext gameContext,
            InputContext inputContext)
        {
            _stateMachine = stateMachine;
            _systemEngine = systemEngine;
            _gameContext = gameContext;
            _inputContext = inputContext;
        }

        public void Enter()
        {
            _systemEngine.RegisterSystem(new PlayerInputSystem(_inputContext));
            _systemEngine.RegisterSystem(new CameraMovableSystem(_gameContext, _inputContext));
            _systemEngine.RegisterSystem(new MouseInputSystem(_inputContext, _gameContext));
            _systemEngine.RegisterSystem(new ReachDestinationSystem(_gameContext, _inputContext));

            _systemEngine.Start();

            _stateMachine.Enter<LoadMapState>();
        }

        public void Exit()
        {
        }
    }
}